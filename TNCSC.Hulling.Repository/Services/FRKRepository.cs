#region Referefnces
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.FRK;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// FRKRepository
    /// </summary>
    public class FRKRepository : BaseRepository, IFRKRepository
    {
        #region Constructor
        public FRKRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }
        #endregion

        #region AddFRKDetails
        public async Task<APIResponse> AddFRKDetails(FRK frkObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (frkObj != null)
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@userId", frkObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millId", frkObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", frkObj.GodwonRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", frkObj.Date, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@kms", frkObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@memoIssueNo", frkObj.IssueMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@elnvNo", frkObj.ElnvNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@company", frkObj.CompanyName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfBags", frkObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@weight", frkObj.WeightOfFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@lorryNo", frkObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@trasport", frkObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddFRKDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.FRKDetailsAddedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddFRKDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "FRKRepository:AddFRKDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingFRKDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingFRKDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingFRKDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            APIResponse aPIResponse = new APIResponse();
            List<FRK> details = new List<FRK>();
            aPIResponse.version = sVersion;
            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<FRK>((SqlConnection)connection, "SP_GetAllFRKDetails", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    details = response.ToList();
                    aPIResponse.responseCode = ResponseCode.FRKDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoFRKDetailsFound;
                }

                aPIResponse.data = details;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "FRKRepository:GetAllDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetFRKDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetFRKDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetFRKDetails);
                return aPIResponse;
                throw;
            }
        }
        #endregion

        #region GetFRKDetailsByMillId
        public async Task<APIResponse> GetFRKDetailsByMillId(long millId)
        {
            APIResponse aPIResponse = new APIResponse();
            List<FRK> details = new List<FRK>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millId", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<FRK>((SqlConnection)connection, "SP_GetFRKDetailsByMillId", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    details = response.ToList();
                    aPIResponse.responseCode = ResponseCode.FRKDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoFRKDetailsFound;
                }
                aPIResponse.data = details;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "FRKRepository:GetFRKDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetFRKDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetFRKDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetFRKDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetFRKDetailsById
        public async Task<APIResponse> GetFRKDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            FRK details = new FRK();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@frkId", id, DbType.String, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryFirstOrDefaultAsync<FRK>((SqlConnection)connection, "SP_GetFRKDetailsById", parameters, commandType: CommandType.StoredProcedure);
                if (response != null)
                {
                    details = response;
                    aPIResponse.responseCode = ResponseCode.FRKDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoPaddyDetailsFound;
                }
                aPIResponse.data = details;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "FRKRepository:GetFRKDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetFRKDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetFRKDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetFRKDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region UpdateFRKDetails
        public async Task<APIResponse> UpdateFRKDetails(FRK frkObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (frkObj != null)
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", frkObj.Id, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@userId", frkObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millId", frkObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", frkObj.GodwonRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", frkObj.Date, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@kms", frkObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@memoIssueNo", frkObj.IssueMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@elnvNo", frkObj.ElnvNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@company", frkObj.CompanyName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfBags", frkObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@weight", frkObj.WeightOfFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@lorryNo", frkObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@trasport", frkObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdateFRKDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@_id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.FRKDetailsUpdatedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToUpdateFRKDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "FRKRepository:UpdateFRKDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUpdatingFRKDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUpdatingFRKDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUpdatingFRKDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

    }
}
