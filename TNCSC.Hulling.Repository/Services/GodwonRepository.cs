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
using TNCSC.Hulling.Domain.Godwon;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// GodwonRepository
    /// </summary>
    public class GodwonRepository : BaseRepository, IGodwonRepository
    {
        #region Constructor
        public GodwonRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }
        #endregion

        #region AddNewGodwon
        public async Task<APIResponse> AddNewGodwon(Godwon godwonObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (godwonObj != null)
                {
                    string code = IdGeneration.GetDistrictCode(godwonObj.District);
                    godwonObj.GodwonId = code + "G";

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@userId", godwonObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godwonId", godwonObj.GodwonId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godwonName", godwonObj.GodwonName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godwonType", godwonObj.godwonType, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@millId", godwonObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@emailId", godwonObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", godwonObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@gstNo", godwonObj.GSTNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@country", godwonObj.Country, DbType.String, ParameterDirection.Input);
                    parameters.Add("@state", godwonObj.State, DbType.String, ParameterDirection.Input);
                    parameters.Add("@district", godwonObj.District, DbType.String, ParameterDirection.Input);
                    parameters.Add("@region", godwonObj.Region, DbType.String, ParameterDirection.Input);
                    parameters.Add("@address", godwonObj.Address, DbType.String, ParameterDirection.Input);
                    parameters.Add("@distance", godwonObj.Distance, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@aqName", godwonObj.AQName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@superintendent", godwonObj.Superintendent, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);
                    parameters.Add("@gId", "", DbType.String, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddNewGodWon", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");
                    var userID = parameters.Get<string>("@gId");
                    IDictionary<string, string> response = new Dictionary<string, string>();
                    if (Id != 0 && !string.IsNullOrEmpty(userID))
                    {
                        response.Add("id", Id.ToString());
                        response.Add("gId", userID);
                    }
                    aPIResponse.data = response;
                    aPIResponse.responseCode = ResponseCode.GodwonAddedSuccessfully;
                    return aPIResponse;

                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToAddGodwon;
                    return aPIResponse;
                }


            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodwonRepository:AddNewGodwon";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingGodwon;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingGodwon;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingGodwon);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetAllGodwons
        public async Task<APIResponse> GetAllGodwons()
        {
            APIResponse aPIResponse = new APIResponse();
            List<GodwonDetails> godwons = new List<GodwonDetails>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<GodwonDetails>((SqlConnection)connection, "SP_GetAllGodwons", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    godwons = response.ToList();
                    aPIResponse.responseCode = ResponseCode.GodwonDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGodwonsFound;
                }
                aPIResponse.data = godwons;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodwonRepository:GetAllGodwons";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGodwonDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGodwonDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGodwonDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetGodwonDetailsByMillId
        public async Task<APIResponse> GetGodwonDetailsByMillId(long millId,int typeId)
        {
            APIResponse aPIResponse = new APIResponse();
            List<GodwonDetails> godwons = new List<GodwonDetails>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millId", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@typeId", typeId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<GodwonDetails>((SqlConnection)connection, "SP_GetGodwonByMillId", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    godwons = response.ToList();
                    aPIResponse.responseCode = ResponseCode.GodwonDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGodwonsFound;
                }
                aPIResponse.data = godwons;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodwonRepository:GetGodwonDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGodwonDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGodwonDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGodwonDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetGodwonDetailsById
        public async Task<APIResponse> GetGodwonDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            GodwonDetails? godwons = new GodwonDetails();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@godwonId", id, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<GodwonDetails>((SqlConnection)connection, "SP_GetGodwonDetailsById", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    godwons = response.SingleOrDefault();
                    aPIResponse.responseCode = ResponseCode.GodwonDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGodwonsFound;
                }
                aPIResponse.data = godwons;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodwonRepository:GetGodwonDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGodwonDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGodwonDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGodwonDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region UpdateGodwonDetails

        public async Task<APIResponse> UpdateGodwonDetails(Godwon godwonObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (godwonObj != null)
                { 

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", godwonObj.Id, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@userId", godwonObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godwonId", godwonObj.GodwonId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godwonName", godwonObj.GodwonName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@millId", godwonObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@emailId", godwonObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", godwonObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@gstNo", godwonObj.GSTNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@country", godwonObj.Country, DbType.String, ParameterDirection.Input);
                    parameters.Add("@state", godwonObj.State, DbType.String, ParameterDirection.Input);
                    parameters.Add("@district", godwonObj.District, DbType.String, ParameterDirection.Input);
                    parameters.Add("@region", godwonObj.Region, DbType.String, ParameterDirection.Input);
                    parameters.Add("@address", godwonObj.Address, DbType.String, ParameterDirection.Input);
                    parameters.Add("@distance", godwonObj.Distance, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@aqName", godwonObj.AQName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@superintendent", godwonObj.Superintendent, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdateGodWonDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@_id");
                     
                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.GodwonUpdatedSuccessfully;
                    return aPIResponse;

                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToUpdateGodwon;
                    return aPIResponse;
                }


            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GodwonRepository:UpdateGodwonDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUpdatingGodwon;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUpdatingGodwon;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUpdatingGodwon);
                return aPIResponse;
                throw;
            }

        }
        #endregion
    }

}
