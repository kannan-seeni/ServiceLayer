using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Components.Options;

namespace TNCSC.Hulling.Components.Services
{
    public class Logger : ILogger
    {


        private string[] _LogTargetArray;

        private string[] _LogTypeArray;

        private string _errorLogFolder;

        private bool bIsLogTextFile = false;

        private bool bIsLogCSVFile = false;

        private bool bIsLogDB = false;

        private string sConnectionString = string.Empty;

        private string sLogFolder = @"\Logs";

        private LoggerOptions _loggerOptions;


        private IConfigManager _configManager;


        #region Constructor
        /// <summary>
        /// Logger
        /// </summary>
        /// <param name="options"></param>
        public Logger(IConfigManager configManager, IOptions<LoggerOptions> options)
        {
            _configManager = configManager;
            _loggerOptions = options.Value;
            LoadloggerConfig(_loggerOptions.ErrorType, _loggerOptions.TargetType, _loggerOptions.FolderPath, _loggerOptions.ConfigConnectionstring);
        }
        #endregion Constructor

        #region Log

        /// <summary>
        /// Msi Method to log error 
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="msiErrorLogModel"></param>
        public void Log(LogType errorType, LoggerModel ErrorLogModel, [CallerMemberName] string methodName = "",
                                        [CallerFilePath] string fileName = "")
        {
            var sErrorTypeValue = GetErrorType(Convert.ToInt32(errorType));
            ErrorLogModel.ActionName = methodName;
            ErrorLogModel.ControllerName = fileName;
            bool IsConfigErrorLogType = Array.Exists(_LogTypeArray, e => e == sErrorTypeValue);

            if (IsConfigErrorLogType && (sErrorTypeValue.Equals("Success") || sErrorTypeValue.Equals("Error") || sErrorTypeValue.Equals("Failed") || sErrorTypeValue.Equals("Info") || sErrorTypeValue.Equals("Warning") || sErrorTypeValue.Equals("IPSettings")))
            {
                InsertLogInDBorFile(errorType, ErrorLogModel);
            }
            else
            {
                if (sErrorTypeValue.Equals("Error") || sErrorTypeValue.Equals("Failed") || sErrorTypeValue.Equals("IPSettings"))
                {
                    InsertLogInDBorFile(errorType, ErrorLogModel);
                }
            }
        }

        #endregion LoadloggerConfig

        #region LoadloggerConfig

        /// <summary>
        /// LoadloggerConfig
        /// </summary>
        /// <param name="sErrorType"></param>
        /// <param name="sTargetType"></param>
        /// <param name="sFolderPath"></param>
        /// <param name="sConfigConnectionstring"></param>
        private void LoadloggerConfig(string sErrorType, string sTargetType, string sFolderPath, string sConfigConnectionstring)
        {
            try
            {
                _LogTypeArray = sErrorType?.Split('|');

                _errorLogFolder = sFolderPath;

                if (!string.IsNullOrEmpty(sFolderPath))
                {
                    _errorLogFolder = string.Concat(sFolderPath, sLogFolder);
                }
                else
                {
                    _errorLogFolder = string.Concat(Environment.CurrentDirectory, sLogFolder);

                }

                _LogTargetArray = sTargetType?.Split('|');

                bIsLogTextFile = Array.Exists(_LogTargetArray, e => e == "Text");
                bIsLogCSVFile = Array.Exists(_LogTargetArray, e => e == "CSV");
                bIsLogDB = Array.Exists(_LogTargetArray, e => e == "DB");

                sConnectionString = sConfigConnectionstring;

            }

            catch (Exception)
            {
                throw;
            }
        }
        #endregion LoadloggerConfig

        #region InsertLogInDBorFile
        /// <summary>
        /// InsertLogInDBorFile
        /// </summary>
        /// <param name="errorType"></param>
        /// <param name="msiErrorLogModel"></param>
        private void InsertLogInDBorFile(LogType errorType, LoggerModel msiErrorLogModel)
        {

            string filepath = string.Empty;

            //if (bIsLogCSVFile)
            //{
            //    // Log the detils into file in IIS folder
            //    filepath = GetErrorLogFolder() + "/" + DateTime.UtcNow.Date.ToString("dd-MM-yyyy") + ".csv";
            //    CreateandLogContent(errorType, msiErrorLogModel, filepath);

            //}

            if (bIsLogTextFile)
            {
                filepath = GetErrorLogFolder() + "/" + DateTime.UtcNow.Date.ToString("dd-MM-yyyy") + ".txt";
                // Log the detils into file in IIS folder
                CreateandLogContent(errorType, msiErrorLogModel, filepath);
            }
            if (bIsLogDB)
            {

                // Log the detils into the database
                InsertLogInDB(errorType, msiErrorLogModel, sConnectionString);

            }

        }
        #endregion InsertLogInDBorFile

        #region GetErrorLogFolder
         
        private string GetErrorLogFolder()
        {
            try
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                string year = dateTime.ToString("yyyy");
                string month = dateTime.ToString("MMM");
                string YearFolder = string.Concat(_errorLogFolder, @"\", year);
                string Monthfolder = YearFolder + @"\" + month;
                bool isExists = Directory.Exists(YearFolder);
                bool isExistsSub = Directory.Exists(Monthfolder);
                if (!isExists)
                {
                    Directory.CreateDirectory(YearFolder);
                }
                if (!isExistsSub)
                {
                    Directory.CreateDirectory(Monthfolder);
                }
                return Monthfolder;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion GetErrorLogFolder

        #region CreateandLogContent
        
        private void CreateandLogContent(LogType errorType, LoggerModel msiErrorLogModel, string filepath)
        {
            var strStackTrace = string.Empty;
            var CreatedOn = DateTime.UtcNow.ToString("dd-MM-yyyy hh:mm:ss");
            var exType = GetErrorType(Convert.ToInt32(errorType));

            StringBuilder sbErrorText = new StringBuilder();

            try
            {
                sbErrorText.AppendLine("LogDateTime :").Append(CreatedOn);
                sbErrorText.AppendLine("UserID : " + msiErrorLogModel.UserID);

                sbErrorText.AppendLine("ErrorType : " + GetErrorType(Convert.ToInt32(errorType)));
                sbErrorText.AppendLine("ResponseCode : " + msiErrorLogModel.ResponseCode);
                sbErrorText.AppendLine("Additional Info : " + msiErrorLogModel.AdditionalInfo);
                sbErrorText.AppendLine("ExecptionMessage : " + msiErrorLogModel.ExecptionDetails?.Message.ToString());

                if (msiErrorLogModel.ExecptionDetails?.StackTrace != null)
                {
                    sbErrorText.AppendLine("StackTrace : " + msiErrorLogModel.ExecptionDetails?.StackTrace.ToString());
                }
                sbErrorText.AppendLine("RaisedBy : " + msiErrorLogModel.UserID);
                sbErrorText.AppendLine("RaisedDate : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sbErrorText.AppendLine("----------------------------------------------------------------------------------");
                File.AppendAllText(filepath, sbErrorText.ToString());
            }
            catch (Exception)
            {


            }
        }
        #endregion CreateandLogContent

        #region InsertLogInDB
        private void InsertLogInDB(LogType errorType, LoggerModel loggerModel, string ConnectionString)
        {
            try
            {
                if (!string.IsNullOrEmpty(loggerModel?.UserID))
                    InsertLog(errorType, loggerModel, ConnectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion InsertLogInDB

        #region GetErrorType
        /// <summary>
        /// GetErrorType
        /// </summary>
        /// <param name="enumID"></param>
        /// <returns></returns>
        private string GetErrorType(int enumID)
        {
            switch (enumID)
            {
                case 0:
                    return "Success";
                case 1:
                    return "Failed";
                case 2:
                    return "Error";
                case 3:
                    return "Info";
                case 4:
                    return "Warning";
                case 5:
                    return "IPSettings";
                default:
                    return "Others";
            }
        }
        #endregion GetErrorType

        #region ReLoadLoggerConfigData
        public void ReLoadLoggerConfigData()
        {
            LoadloggerConfig(_configManager.GetConfig("Logger.Type"),
                _configManager.GetConfig("Logger.Target"),
                _configManager.GetConfig("Logger.FolderPath"),
                _configManager.GetConfig("Logger.ConnectionString"));
        }
        #endregion

        #region InsertLog
        public void InsertLog(LogType errorType, LoggerModel loggerModel, string connectionString)
        {
            try
            {
                if (!string.IsNullOrEmpty(loggerModel?.UserID))
                {
                    var RaisedDate = DateTime.UtcNow;

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserID", !string.IsNullOrEmpty(loggerModel.UserID) ? loggerModel.UserID : "");
                    parameters.Add("@LogType", !string.IsNullOrEmpty(Convert.ToString(errorType)) ? Convert.ToInt32(errorType) : "");
                    parameters.Add("@AdditionalInfo", !string.IsNullOrEmpty(loggerModel.AdditionalInfo) ? loggerModel.AdditionalInfo : "");
                    parameters.Add("@ActionName", !string.IsNullOrEmpty(loggerModel.ActionName) ? loggerModel.ActionName : "");
                    parameters.Add("@ControllerName", !string.IsNullOrEmpty(loggerModel.ControllerName) ? loggerModel.ControllerName : "");
                    parameters.Add("@ExceptionMessage", !string.IsNullOrEmpty(loggerModel.ExecptionDetails?.Message?.ToString()) ? loggerModel.ExecptionDetails?.Message?.ToString() : "");
                    parameters.Add("@Stacktrace", !string.IsNullOrEmpty(loggerModel.ExecptionDetails?.StackTrace?.ToString()) ? loggerModel.ExecptionDetails?.StackTrace?.ToString() : "");
                    parameters.Add("@ResponseCode", !string.IsNullOrEmpty(Convert.ToString(loggerModel.ResponseCode)) ? loggerModel.ResponseCode : 0);
                    parameters.Add("@RaisedBy", !string.IsNullOrEmpty(loggerModel.CreatedBy) ? loggerModel.CreatedBy : "");
                    parameters.Add("@RaisedDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

                    using (var con = new SqlConnection(connectionString))
                    {
                        SqlMapper.Execute(con, "USP_InsertLogDetails", param: parameters, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
