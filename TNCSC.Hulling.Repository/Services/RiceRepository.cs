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
using TNCSC.Hulling.Domain.Rice;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// RiceRepository
    /// </summary>
    public class RiceRepository : BaseRepository, IRiceRepository
    {

        #region Constructor
        public RiceRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {


        }
        #endregion

        #region AddRiceDetails
        public async Task<APIResponse> AddRiceDetails(Rice riceObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (riceObj != null)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@userId", riceObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millId", riceObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", riceObj.GodownRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", riceObj.Date, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@kms", riceObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@truckMomeNo", riceObj.TruckMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godown", riceObj.Godown, DbType.String, ParameterDirection.Input);
                    parameters.Add("@variety", riceObj.Variety, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfBags", riceObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@weightOfRice", riceObj.WeightOfRice, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@weightOfFRK", riceObj.WeightOfFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@weightOfRiceWithFRK", riceObj.WeightOfRiceWithFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@adNo", riceObj.ADNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@adDate", riceObj.ADDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@qcmoitureContent", riceObj.QCMoitureContent, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@qcNo", riceObj.QCNo, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@qcDeHusted", riceObj.QCDeHusted, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@qcFRK", riceObj.QCFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@lorryNo", riceObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfONBBags", riceObj.NoOfONBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSSBags", riceObj.NoOfSSBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSWPBags", riceObj.NoOfSWPBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@isFRKAddted", riceObj.IsFRKAdded, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@frk", riceObj.FRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@depositMonth", riceObj.DepositMonth, DbType.String, ParameterDirection.Input);
                    parameters.Add("@trasport", riceObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddRiceDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.RiceDetailsAddedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddRiceDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "RiceRepository:AddRiceDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingRiceDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingRiceDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingRiceDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            APIResponse aPIResponse = new APIResponse();
            List<RiceDetails> riceDetails = new List<RiceDetails>();
            aPIResponse.version = sVersion;
            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<RiceDetails>((SqlConnection)connection, "SP_GetAllRiceDetails", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    riceDetails = response.ToList();
                    aPIResponse.responseCode = ResponseCode.RiceDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoRiceDetailsFound;
                }

                aPIResponse.data = riceDetails;


                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "RiceRepository:GetAllDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetRiceDetails);
                return aPIResponse;
                throw;
            }
        }
        #endregion

        #region GetRiceDetailsByMillId
        public async Task<APIResponse> GetRiceDetailsByMillId(long millId)
        {
            APIResponse aPIResponse = new APIResponse();
            List<RiceDetails> riceDetails = new List<RiceDetails>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millId", millId, DbType.String, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<RiceDetails>((SqlConnection)connection, "SP_GetRiceDetailsByMillId", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    riceDetails = response.ToList();
                    aPIResponse.responseCode = ResponseCode.RiceDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoRiceDetailsFound;
                }
                aPIResponse.data = riceDetails;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "RiceRepository:GetRiceDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetRiceDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetRiceDetailsById
        public async Task<APIResponse> GetRiceDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            RiceDetails riceDetails = new RiceDetails();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@riceId", id, DbType.String, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryFirstOrDefaultAsync<RiceDetails>((SqlConnection)connection, "SP_GetRiceDetailsById", parameters, commandType: CommandType.StoredProcedure);
                if (response != null)
                {
                    riceDetails = response;
                    aPIResponse.responseCode = ResponseCode.RiceDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoRiceDetailsFound;
                }
                aPIResponse.data = riceDetails;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "RiceRepository:GetRiceDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetRiceDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region UpdateRiceDetails
        public async Task<APIResponse> UpdateRiceDetails(Rice riceObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (riceObj != null)
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", riceObj.Id, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@userId", riceObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millId", riceObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", riceObj.GodownRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", riceObj.Date, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@kms", riceObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@truckMomeNo", riceObj.TruckMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@godown", riceObj.Godown, DbType.String, ParameterDirection.Input);
                    parameters.Add("@variety", riceObj.Variety, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfBags", riceObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@weightOfRice", riceObj.WeightOfRice, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@weightOfFRK", riceObj.WeightOfFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@weightOfRiceWithFRK", riceObj.WeightOfRiceWithFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@adNo", riceObj.ADNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@adDate", riceObj.ADDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@qcmoitureContent", riceObj.QCMoitureContent, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@qcNo", riceObj.QCNo, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@qcDeHusted", riceObj.QCDeHusted, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@qcFRK", riceObj.QCFRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@lorryNo", riceObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfONBBags", riceObj.NoOfONBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSSBags", riceObj.NoOfSSBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSWPBags", riceObj.NoOfSWPBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@frk", riceObj.FRK, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@depositMonth", riceObj.DepositMonth, DbType.String, ParameterDirection.Input);
                    parameters.Add("@isFRKAddted", riceObj.IsFRKAdded, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@trasport", riceObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdateRiceDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@_id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.RiceDetailsUpdatedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToUpdateRiceDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "RiceRepository:UpdateRiceDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUpdatinRiceDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUpdatinRiceDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUpdatinRiceDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region RiceMonthReport
        public async Task<APIResponse> RiceMonthReport(long millId, string month)
        {
            APIResponse aPIResponse = new APIResponse();
            List<RiceReportDetails> adtRiceDetails = new List<RiceReportDetails>();
            List<RiceReportDetails> crRiceDetails = new List<RiceReportDetails>();
            RiceMonthReport riceMonthReport = new RiceMonthReport();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millRefID", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@month", month, DbType.String, ParameterDirection.Input);
                parameters.Add("@adtcount", -1, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@crcount", -1, DbType.Int32, ParameterDirection.Output);

                var response = await SqlMapper.QueryMultipleAsync((SqlConnection)connection, "SP_GetRiceReport", parameters, commandType: CommandType.StoredProcedure);

                adtRiceDetails = response.Read<RiceReportDetails>().ToList();
                if(adtRiceDetails != null && adtRiceDetails.Count > 0)
                {
                    riceMonthReport.adtMonthReport = new RiceReport();
                    ReportData adtTotal = new ReportData();
                    List<GodownTotal> adtGodwonTotal = new List<GodownTotal>();
                    adtTotal = response.Read<ReportData>().First();
                    adtGodwonTotal = response.Read<GodownTotal>().ToList();
                    riceMonthReport.adtMonthReport.details = adtRiceDetails;
                    riceMonthReport.adtMonthReport.total = adtTotal;
                    riceMonthReport.adtMonthReport.godownTotal = adtGodwonTotal;
                }
                
                crRiceDetails = response.Read<RiceReportDetails>().ToList();

                if (crRiceDetails != null && crRiceDetails.Count() > 0)
                {
                    riceMonthReport.crMonthReport = new RiceReport();
                    ReportData crTotal = new ReportData();
                    List<GodownTotal> crGodwonTotal = new List<GodownTotal>();
                    crTotal = response.Read<ReportData>().First();
                    crGodwonTotal = response.Read<GodownTotal>().ToList();
                    riceMonthReport.crMonthReport.details = crRiceDetails;
                    riceMonthReport.crMonthReport.total = crTotal;
                    riceMonthReport.crMonthReport.godownTotal = crGodwonTotal;
                }
                   

                aPIResponse.data = riceMonthReport;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "RiceRepository:GetRiceDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetRiceDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetRiceDetails);
                return aPIResponse;
                throw;
            }

        }

        #endregion
    }
}
