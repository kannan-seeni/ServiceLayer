#region References
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Components.Options;
#endregion

namespace TNCSC.Hulling.Components.Services
{
    /// <summary>
    /// AuditManager
    /// </summary>
    public class AuditManager : IAuditManager
    {
        #region Declarations
        private AuditModel auditModel;

        private bool bIsAuditEnabled = false;

        private bool bIsPayLoadEnabled = false;

        private string sConnectionString;

        private AuditAttributeOptions _auditAttributeOptions;

        private IConfigManager _configManager;
        #endregion

        #region Constructor
        public AuditManager(IConfigManager configManager, IOptions<AuditAttributeOptions> options)
        {
            _configManager = configManager;
            _auditAttributeOptions = options.Value;

            sConnectionString = _auditAttributeOptions.ConnectionString;
            bIsAuditEnabled = _auditAttributeOptions.IsAuditEnabled;
            bIsPayLoadEnabled = _auditAttributeOptions.IsPayloadRecorded;
        }
        #endregion

        #region Log
        public bool Log(AuditModel _auditModel)
        {
            try
            {
                if (bIsAuditEnabled)
                {
                    auditModel = _auditModel;
                    if (auditModel != null)
                    {
                        if (!string.IsNullOrEmpty(sConnectionString) && bIsAuditEnabled)
                            InsertAuditLogs(auditModel, sConnectionString);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region LoadAuditConfigData
        public void LoadAuditConfigData(string ConnectionString)
        {
            sConnectionString = ConnectionString;
        }
        #endregion

        #region LoadAuditConfigData
        public void ReLoadAuditConfigData()
        {
            sConnectionString = _configManager.GetConfig("AuditLog.DefaultConnection");
            bIsAuditEnabled = Convert.ToBoolean(_configManager.GetConfig("AuditLog.isEnabled"));
            bIsPayLoadEnabled = Convert.ToBoolean(_configManager.GetConfig("AuditLog.PayLoad"));
        }
        #endregion

        #region GetAuditType
        private string GetAuditType(int enumID)
        {
            switch (enumID)
            {
                case 0:
                    return "Entry";
                case 1:
                    return "Exit";
                case 2:
                    return "Get";
                case 3:
                    return "Update";
                case 4:
                    return "Delete";
                default:
                    return "Others";
            }
        }
        #endregion

        #region InsertAuditLogs
        public void InsertAuditLogs(AuditModel objauditmodel, string sConnectionString)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@MethodName", !string.IsNullOrEmpty(objauditmodel.MethodName) ? objauditmodel.MethodName : "");
                parameters.Add("@ControllerName", !string.IsNullOrEmpty(objauditmodel.ControllerName) ? objauditmodel.ControllerName : "");
                parameters.Add("@ActionName", !string.IsNullOrEmpty(objauditmodel.ActionName) ? objauditmodel.ActionName : "");
                parameters.Add("@Action", !string.IsNullOrEmpty(objauditmodel.Action) ? objauditmodel.Action : "");
                parameters.Add("@URLReferrer", !string.IsNullOrEmpty(objauditmodel.URLReferrer) ? objauditmodel.URLReferrer : "");
                parameters.Add("@IPAddress", !string.IsNullOrEmpty(objauditmodel.IPAddress) ? objauditmodel.IPAddress : "");
                parameters.Add("@SessionID", !string.IsNullOrEmpty(objauditmodel.@SessionID) ? objauditmodel.@SessionID : "");
                parameters.Add("@UserID", !string.IsNullOrEmpty(objauditmodel.UserID) ? objauditmodel.UserID : "");
                parameters.Add("@UserName", !string.IsNullOrEmpty(objauditmodel.UserName) ? Convert.ToString(objauditmodel.UserName) : "");
                parameters.Add("@TimeAccessed", !string.IsNullOrEmpty(Convert.ToString(objauditmodel.TimeAccessed)) ? Convert.ToString(objauditmodel.TimeAccessed) : "");
                parameters.Add("@LoginStatus", !string.IsNullOrEmpty(objauditmodel.LoginStatus) ? objauditmodel.LoginStatus : "");
                parameters.Add("@LoggedInAt", !string.IsNullOrEmpty(objauditmodel.LoggedInAt) ? objauditmodel.LoggedInAt : "");
                parameters.Add("@LoggedOutAt", !string.IsNullOrEmpty(objauditmodel.LoggedOutAt) ? objauditmodel.LoggedOutAt : "");
                parameters.Add("@PayLoad", !string.IsNullOrEmpty(Convert.ToString(objauditmodel.PayLoad)) ? Convert.ToString(objauditmodel.PayLoad) : "");
                parameters.Add("@CreatedBy", !string.IsNullOrEmpty(objauditmodel.CreatedBy) ? objauditmodel.CreatedBy : "");
                parameters.Add("@CreatedDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

                using (var con = new SqlConnection(sConnectionString))
                {
                    con.Open();
                    con.Execute("USP_InsertAuditLogs", parameters, null, 0, CommandType.StoredProcedure);
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
