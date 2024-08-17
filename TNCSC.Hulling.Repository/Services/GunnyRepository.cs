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
using TNCSC.Hulling.Domain.Gunnys;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// GunnyRepository
    /// </summary>
    public class GunnyRepository : BaseRepository, IGunnyRepository
    {
        #region Constructor
        public GunnyRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }
        #endregion

        #region AddGunnyDetails
        public async Task<APIResponse> AddGunnyDetails(Gunny gunnyObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (gunnyObj != null)
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@userRefId", gunnyObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millRefId", gunnyObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownRefId", gunnyObj.GodwonRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", gunnyObj.Date, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@kms", gunnyObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@truckMomeNo", gunnyObj.TruckMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godown", gunnyObj.Godwon, DbType.String, ParameterDirection.Input); 
                    parameters.Add("@noOfBags", gunnyObj.NoOfBags, DbType.Int32, ParameterDirection.Input); 
                    parameters.Add("@adNo", gunnyObj.ADNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@adDate", gunnyObj.ADDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@lorryNo", gunnyObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfONBBags", gunnyObj.NoOfONBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSSBags", gunnyObj.NoOfSSBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSWPBags", gunnyObj.NoOfSWPBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@trasport", gunnyObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddGunnyDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.GunnyDetailsAddedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddGunnyDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GunnyRepository:AddGunnyDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingGunnyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingGunnyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingGunnyDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            APIResponse aPIResponse = new APIResponse();
            List<Gunny> gunnyDetails = new List<Gunny>();
            aPIResponse.version = sVersion;
            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<Gunny>((SqlConnection)connection, "SP_GetAllGunnyDetails", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    gunnyDetails = response.ToList();
                    aPIResponse.responseCode = ResponseCode.GunnyDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGunnyDetailsFound;
                }

                aPIResponse.data = gunnyDetails;


                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GunnyRepository:GetAllDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGunnyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGunnyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGunnyDetails);
                return aPIResponse;
                throw;
            }
        }
        #endregion

        #region GetGunnyDetailsByMillId
        public async Task<APIResponse> GetGunnyDetailsByMillId(long millId)
        {
            APIResponse aPIResponse = new APIResponse();
            List<Gunny> gunnyDetails = new List<Gunny>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millRefId", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<Gunny>((SqlConnection)connection, "SP_GetGunnyDetailsByMillId", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    gunnyDetails = response.ToList();
                    aPIResponse.responseCode = ResponseCode.GunnyDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGunnyDetailsFound;
                }
                aPIResponse.data = gunnyDetails;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GunnyRepository:GetGunnyDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGunnyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGunnyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGunnyDetails);
                return aPIResponse;
                throw;
            }

        }
#endregion

        #region GetGunnyDetailsById
        public async Task<APIResponse> GetGunnyDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            Gunny gunnyDetails = new Gunny();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@gunnyId", id, DbType.String, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryFirstOrDefaultAsync<Gunny>((SqlConnection)connection, "SP_GetGunnyDetailsById", parameters, commandType: CommandType.StoredProcedure);
                if (response != null)
                {
                    gunnyDetails = response;
                    aPIResponse.responseCode = ResponseCode.GunnyDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGunnyDetailsFound;
                }
                aPIResponse.data = gunnyDetails;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GunnyRepository:GetGunnyDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGunnyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGunnyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGunnyDetails);
                return aPIResponse;
                throw;
            }

        }
#endregion

        #region UpdateGunnyDetails
        public async Task<APIResponse> UpdateGunnyDetails(Gunny gunnyObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (gunnyObj != null)
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", gunnyObj.Id, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@userRefId", gunnyObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millRefId", gunnyObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownRefId", gunnyObj.GodwonRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", gunnyObj.Date, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@kms", gunnyObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@truckMomeNo", gunnyObj.TruckMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godown", gunnyObj.Godwon, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfBags", gunnyObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@adNo", gunnyObj.ADNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@adDate", gunnyObj.ADDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@lorryNo", gunnyObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfONBBags", gunnyObj.NoOfONBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSSBags", gunnyObj.NoOfSSBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSWPBags", gunnyObj.NoOfSWPBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@trasport", gunnyObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdateGunnyDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@_id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.GunnyDetailsUpdatedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToUpdateGunnyDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "GunnyRepository:UpdateGunnyDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUpdatingGunnyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUpdatingGunnyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUpdatingGunnyDetails);
                return aPIResponse;
                throw;
            }

        }
#endregion

    }
}
