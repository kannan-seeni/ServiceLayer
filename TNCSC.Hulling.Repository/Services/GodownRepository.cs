#region References
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Godown;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// GodwonRepository
    /// </summary>
    public class GodownRepository : BaseRepository, IGodownRepository
    {
        #region Constructor
        public GodownRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }
        #endregion

        #region AddNewGodwon
        public async Task<APIResponse> AddNewGodown(Godown godownObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (godownObj != null)
                {
                    string code = IdGeneration.GetDistrictCode(godownObj.District);
                    godownObj.GodownId = code + "G";

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@userId", godownObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", godownObj.GodownId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godownName", godownObj.GodownName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godownType", godownObj.godownType, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@regionId", godownObj.RegionId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@emailId", godownObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", godownObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@gstNo", godownObj.GSTNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@country", godownObj.Country, DbType.String, ParameterDirection.Input);
                    parameters.Add("@state", godownObj.State, DbType.String, ParameterDirection.Input);
                    parameters.Add("@district", godownObj.District, DbType.String, ParameterDirection.Input);
                    parameters.Add("@region", godownObj.Region, DbType.String, ParameterDirection.Input);
                    parameters.Add("@address", godownObj.Address, DbType.String, ParameterDirection.Input);
                    parameters.Add("@distance", godownObj.Distance, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@aqName", godownObj.AQName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@superintendent", godownObj.Superintendent, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);
                    parameters.Add("@gId", "", DbType.String, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddNewGodown", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");
                    var userID = parameters.Get<string>("@gId");
                    IDictionary<string, string> response = new Dictionary<string, string>();
                    if (Id != 0 && !string.IsNullOrEmpty(userID))
                    {
                        response.Add("id", Id.ToString());
                        response.Add("gId", userID);
                    }
                    aPIResponse.data = response;
                    aPIResponse.responseCode = ResponseCode.GodownAddedSuccessfully;
                    return aPIResponse;

                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToAddGodown;
                    return aPIResponse;
                }


            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodownRepository:AddNewGodown";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingGodown;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingGodown;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingGodown);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetAllGodowns
        public async Task<APIResponse> GetAllGodowns()
        {
            APIResponse aPIResponse = new APIResponse();
            List<GodownDetails> godowns = new List<GodownDetails>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<GodownDetails>((SqlConnection)connection, "SP_GetAllGodowns", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    godowns = response.ToList();
                    aPIResponse.responseCode = ResponseCode.GodownDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGodownsFound;
                }
                aPIResponse.data = godowns;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodownRepository:GetAllGodowns";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGodownDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGodownDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGodownDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetGodownDetailsByRegion
        public async Task<APIResponse> GetGodownDetailsByRegion(int regionId,int typeId)
        {
            APIResponse aPIResponse = new APIResponse();
            List<GodownDetails> godowns = new List<GodownDetails>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@regionId", regionId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@typeId", typeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<GodownDetails>((SqlConnection)connection, "SP_GetGodownByRegion", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    godowns = response.ToList();
                    aPIResponse.responseCode = ResponseCode.GodownDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGodownsFound;
                }
                aPIResponse.data = godowns;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodownRepository:GetGodownDetailsByRegion";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGodownDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGodownDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGodownDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetGodownDetailsById
        public async Task<APIResponse> GetGodownDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            GodownDetails? godowns = new GodownDetails();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@godownId", id, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<GodownDetails>((SqlConnection)connection, "SP_GetGodownDetailsById", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    godowns = response.SingleOrDefault();
                    aPIResponse.responseCode = ResponseCode.GodownDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGodownsFound;
                }
                aPIResponse.data = godowns;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodownRepository:GetGodownDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGodownDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGodownDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGodownDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region UpdateGodownDetails

        public async Task<APIResponse> UpdateGodownDetails(Godown godownObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (godownObj != null)
                { 

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", godownObj.Id, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@userId", godownObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", godownObj.GodownId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godownName", godownObj.GodownName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@regionId", godownObj.RegionId, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@emailId", godownObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", godownObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@gstNo", godownObj.GSTNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@country", godownObj.Country, DbType.String, ParameterDirection.Input);
                    parameters.Add("@state", godownObj.State, DbType.String, ParameterDirection.Input);
                    parameters.Add("@district", godownObj.District, DbType.String, ParameterDirection.Input);
                    parameters.Add("@region", godownObj.Region, DbType.String, ParameterDirection.Input);
                    parameters.Add("@address", godownObj.Address, DbType.String, ParameterDirection.Input);
                    parameters.Add("@distance", godownObj.Distance, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@aqName", godownObj.AQName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@superintendent", godownObj.Superintendent, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdateGodownDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@_id");
                     
                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.GodownUpdatedSuccessfully;
                    return aPIResponse;

                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToUpdateGodown;
                    return aPIResponse;
                }


            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodownRepository:UpdateGodownDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUpdatingGodown;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUpdatingGodown;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUpdatingGodown);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region ActiveOrInActivateGodown
        public async Task<APIResponse> ActiveOrInActivateGodown(long godownId, bool status)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", godownId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", status, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);

                var user = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_ActiveOrInActivateGodown", parameters, commandType: CommandType.StoredProcedure);
                aPIResponse.data = user;
                aPIResponse.responseCode = ResponseCode.ActiveOrInactiveGodownSuccessfully;
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodownRepository:ActiveOrInActivateGodown";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInActiveOrInactiveGodown;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInActiveOrInactiveGodown;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInActiveOrInactiveGodown);
                return aPIResponse;
                throw;
            }


        }
        #endregion
    }

}
