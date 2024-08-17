using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Mill;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;

namespace TNCSC.Hulling.Repository.Services
{
    public class MillRepository : BaseRepository,IMillRepository
    {
        public MillRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }

        #region Methods

        #region AddNewMill
        public async Task<APIResponse> AddNewMill(Mill millObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (millObj != null)
                {
                    string code = IdGeneration.GetDistrictCode(millObj.District);
                    millObj.MillId = code + millObj.MillCode;

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@ownerName", millObj.OwnerName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millName", millObj.MillName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millCode", millObj.MillCode, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millId", millObj.MillId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@emailId", millObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", millObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@gstNo", millObj.GSTNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@country", millObj.Country, DbType.String, ParameterDirection.Input);
                    parameters.Add("@state", millObj.State, DbType.String, ParameterDirection.Input);
                    parameters.Add("@district", millObj.District, DbType.String, ParameterDirection.Input);
                    parameters.Add("@region", millObj.Region, DbType.String, ParameterDirection.Input);
                    parameters.Add("@address", millObj.Address, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@millRefId", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddNewMill", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@millRefId");

                    if (Id == 0)
                    {
                        aPIResponse.data = "The mill is already exists in sever, Please check the mill code";
                        aPIResponse.responseCode = ResponseCode.AlreadyExists;
                    }
                    else
                    {
                        aPIResponse.data = "Added Successfully";
                        aPIResponse.responseCode = ResponseCode.MillDetailsAddedSuccessfully;
                    }


                    return aPIResponse;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToAddMillDetails;
                    return aPIResponse;
                }


            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MillRepository:AddNewMill";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingMillDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingMillDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingMillDetails);
                return aPIResponse;
                throw;
            }


        }

        #endregion

        #region GetUsersByMillId
        public async Task<APIResponse> GetMillDetailsByMillId(long millId)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            Mill millDetails = new Mill();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@millrefId", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var user = await SqlMapper.QueryAsync<Mill>((SqlConnection)connection, "SP_GetMillDetailsByMillId", parameters, commandType: CommandType.StoredProcedure);

                if (user != null && user.Count() > 0)
                {
                    millDetails = user.ToList().SingleOrDefault();
                    aPIResponse.data = millDetails;
                    aPIResponse.responseCode = ResponseCode.MillDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.data = millDetails;
                    aPIResponse.responseCode = ResponseCode.NoMillDetailsFound;
                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MillRepository:GetMillDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetMillDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetMillDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetMillDetails);
                return aPIResponse;
                throw;
            }


        }
        #endregion
         
        #region GetAllMills
        public async Task<APIResponse> GetAllMills()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            List<Mill> millDetails = new List<Mill>();

            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var details = await SqlMapper.QueryAsync<Mill>((SqlConnection)connection, "SP_GetAllMillDetails", parameters, commandType: CommandType.StoredProcedure);

                if (details != null && details.Count() > 0)
                {
                    millDetails = details.ToList();
                    aPIResponse.responseCode = ResponseCode.MillDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoMillDetailsFound;
                }
                aPIResponse.data = millDetails;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MillRepository:GetAllMills";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetMillDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetMillDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetMillDetails);
                return aPIResponse;
                throw;
            }


        }
        #endregion


        public async Task<APIResponse> UpdateMillDetails(Mill millObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (millObj != null)
                { 
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", millObj.ID, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@ownerName", millObj.OwnerName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millName", millObj.MillName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millCode", millObj.MillCode, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millId", millObj.MillId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@emailId", millObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", millObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@gstNo", millObj.GSTNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@country", millObj.Country, DbType.String, ParameterDirection.Input);
                    parameters.Add("@state", millObj.State, DbType.String, ParameterDirection.Input);
                    parameters.Add("@district", millObj.District, DbType.String, ParameterDirection.Input);
                    parameters.Add("@region", millObj.Region, DbType.String, ParameterDirection.Input);
                    parameters.Add("@address", millObj.Address, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input); 
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input); 

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdateMillDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");


                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.MillDetailsAddedSuccessfully;

                    return aPIResponse;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToAddMillDetails;
                    return aPIResponse;
                }


            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MillRepository:UpdateMillDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingMillDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingMillDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingMillDetails);
                return aPIResponse;
                throw;
            }


        }

        #region ActiveOrInActivateMill
        public async Task<APIResponse> ActiveOrInActivateMill(long millId, bool status)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", status, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);

                var user = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_ActiveOrInActivateMill", parameters, commandType: CommandType.StoredProcedure);
                aPIResponse.data = user;
                aPIResponse.responseCode = ResponseCode.ActiveOrInactiveMillSuccessfully;
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MillRepository:ActiveOrInActivateMill";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInActiveOrInactiveMill;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInActiveOrInactiveMill;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInActiveOrInactiveMill);
                return aPIResponse;
                throw;
            }


        }
        #endregion


        #endregion
    }
}
