#region References
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Paddy;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// PaddyRepository
    /// </summary>
    public class PaddyRepository : BaseRepository, IPaddyRepository
    {
        #region Constructor
        public PaddyRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }
        #endregion

        #region AddPaddyDetails
        public async Task<APIResponse> AddPaddyDetails(Paddy paddyObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (paddyObj != null)
                {

                    string formatedMonth = IdGeneration.FormatMonthandYear(paddyObj.PaddyReceivedDate);

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@userId", paddyObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millId", paddyObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", paddyObj.GodownRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", paddyObj.PaddyReceivedDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@month", formatedMonth, DbType.String, ParameterDirection.Input);
                    parameters.Add("@kms", paddyObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@memoIssueNo", paddyObj.IssueMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@variety", paddyObj.Variety, DbType.String, ParameterDirection.Input);
                    parameters.Add("@moitureContent", paddyObj.MoitureContent, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@noOfBags", paddyObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@weight", paddyObj.WeightOfPaddy, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@lorryNo", paddyObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfNBBags", paddyObj.NoOfNBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfONBBags", paddyObj.NoOfONBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSSBags", paddyObj.NoOfSSBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSWPBags", paddyObj.NoOfSWPBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@trasport", paddyObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddPaddyDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.PaddyDetailsAddedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddPaddyDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "PaddyRepository:AddPaddyDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingPaddyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingPaddyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingPaddyDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            APIResponse aPIResponse = new APIResponse();
            List<PaddyDetails> paddyDetails = new List<PaddyDetails>();
            aPIResponse.version = sVersion;
            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<PaddyDetails>((SqlConnection)connection, "SP_GetAllPaddyDetails", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    paddyDetails = response.ToList();
                    aPIResponse.responseCode = ResponseCode.PaddyDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoPaddyDetailsFound;
                }

                aPIResponse.data = paddyDetails;


                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "PaddyRepository:GetAllDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetPaddyDetails);
                return aPIResponse;
                throw;
            }
        }
        #endregion

        #region GetPaddyDetailsByMillId
        public async Task<APIResponse> GetPaddyDetailsByMillId(long millId)
        {
            APIResponse aPIResponse = new APIResponse();
            List<PaddyDetails> paddyDetails = new List<PaddyDetails>();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millId", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<PaddyDetails>((SqlConnection)connection, "SP_GetPaddyDetailsByMillId", parameters, commandType: CommandType.StoredProcedure);
                if (response != null && response.Count() > 0)
                {
                    paddyDetails = response.ToList();
                    aPIResponse.responseCode = ResponseCode.PaddyDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoPaddyDetailsFound;
                }
                aPIResponse.data = paddyDetails;

                return aPIResponse;




            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "PaddyRepository:GetPaddyDetailsByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetPaddyDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region GetPaddyDetailsById
        public async Task<APIResponse> GetPaddyDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            PaddyDetails paddyDetails = new PaddyDetails();
            aPIResponse.version = sVersion;
            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@paddyId", id, DbType.String, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryFirstOrDefaultAsync<PaddyDetails>((SqlConnection)connection, "SP_GetPaddyDetailsById", parameters, commandType: CommandType.StoredProcedure);
                if (response != null)
                {
                    paddyDetails = response;
                    aPIResponse.responseCode = ResponseCode.PaddyDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoPaddyDetailsFound;
                }
                aPIResponse.data = paddyDetails;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "PaddyRepository:GetPaddyDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetPaddyDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion

        #region UpdatePaddyDetails
        public async Task<APIResponse> UpdatePaddyDetails(Paddy paddyObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {

                if (paddyObj != null)
                {
                    string formatedMonth = IdGeneration.FormatMonthandYear(paddyObj.PaddyReceivedDate);
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", paddyObj.Id, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@userId", paddyObj.UserRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@millId", paddyObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@godownId", paddyObj.GodownRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@date", paddyObj.PaddyReceivedDate, DbType.DateTime, ParameterDirection.Input);
                    parameters.Add("@month", formatedMonth, DbType.String, ParameterDirection.Input);
                    parameters.Add("@kms", paddyObj.KMS, DbType.String, ParameterDirection.Input);
                    parameters.Add("@memoIssueNo", paddyObj.IssueMemoNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@variety", paddyObj.Variety, DbType.String, ParameterDirection.Input);
                    parameters.Add("@moitureContent", paddyObj.MoitureContent, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@noOfBags", paddyObj.NoOfBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@weight", paddyObj.WeightOfPaddy, DbType.Decimal, ParameterDirection.Input);
                    parameters.Add("@lorryNo", paddyObj.LorryNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@noOfNBBags", paddyObj.NoOfNBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfONBBags", paddyObj.NoOfONBBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSSBags", paddyObj.NoOfSSBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@noOfSWPBags", paddyObj.NoOfSWPBags, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@trasport", paddyObj.Transport, DbType.String, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int64, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_UpdatePaddyDetails", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int64>("@_id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.PaddyDetailsUpdatedSuccessfully;


                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToUpdatePaddyDetails;

                }
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "PaddyRepository:UpdatePaddyDetails";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUpdatingPaddyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUpdatingPaddyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUpdatingPaddyDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion


        #region PaddyMonthlyReport
        public async Task<APIResponse> PaddyMonthlyReport(long millId, string month)
        {

            APIResponse aPIResponse = new APIResponse();
            PaddyFinalReport finalReport = new PaddyFinalReport();
            List<PaddyReportDetails> ADTDetails = new List<PaddyReportDetails>();
            List<PaddyReportDetails> FRDetails = new List<PaddyReportDetails>();
            aPIResponse.version = sVersion;

            try
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@millRefID", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@month", month, DbType.String, ParameterDirection.Input);
                parameters.Add("@count1", -1, DbType.Int32, ParameterDirection.Output);
                parameters.Add("@count2", -1, DbType.Int32, ParameterDirection.Output);

                var response = await SqlMapper.QueryMultipleAsync((SqlConnection)connection, "SP_GetPaddyReport", parameters, commandType: CommandType.StoredProcedure);
                //var adtRecordCount = parameters.Get<int>("@count1");
                //var crRecordCount = parameters.Get<int>("@count2");
                ADTDetails = response.Read<PaddyReportDetails>().ToList();

                if (finalReport != null)
                {
                    if (ADTDetails != null && ADTDetails.Count() > 0)
                    {
                        List<PaddyReport> adtreport = new List<PaddyReport>();
                        adtreport = PaddyDataFilter(ADTDetails);

                        finalReport.ADTMonthReport = new paddyMonthReport();
                        if (adtreport != null && adtreport.Count() > 0)
                        {
                            finalReport.ADTMonthReport.ReportPerDay = adtreport;
                        }

                        ReportData data = new ReportData();
                        data.Weight = ADTDetails.Sum(x => x.Weight); data.Bags = ADTDetails.Sum(x => x.Bags); data.ONB = ADTDetails.Sum(x => x.ONB);
                        data.NB = ADTDetails.Sum(x => x.NB); data.SS = ADTDetails.Sum(x => x.SS); data.Count = ADTDetails.Count();

                        finalReport.ADTMonthReport.GrandTotal = data;

                    }
                    FRDetails = response.Read<PaddyReportDetails>().ToList();
                    if (FRDetails != null && FRDetails.Count() > 0)
                    {
                        List<PaddyReport> crReport = new List<PaddyReport>();
                        crReport = PaddyDataFilter(FRDetails);
                        finalReport.CRMonthReport = new paddyMonthReport();
                        finalReport.CRMonthReport.ReportPerDay = crReport;
                        ReportData data = new ReportData();

                        data.Weight = FRDetails.Sum(x => x.Weight); data.Bags = FRDetails.Sum(x => x.Bags); data.ONB = FRDetails.Sum(x => x.ONB);
                        data.NB = FRDetails.Sum(x => x.NB); data.SS = FRDetails.Sum(x => x.SS); data.Count = FRDetails.Count();

                        finalReport.CRMonthReport.GrandTotal = data;

                    }
                }

                aPIResponse.data = finalReport;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "PaddyRepository:PaddyMonthlyReport";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetPaddyDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetPaddyDetails);
                return aPIResponse;
                throw;
            }

        }
        #endregion



        public List<PaddyReport> PaddyDataFilter(List<PaddyReportDetails> Details)
        {
            List<PaddyReport> listOfReportPerDay = new List<PaddyReport>();
            PaddyFinalReport finalReport = new PaddyFinalReport();
            List<DateTime> listOfDates = Details.Select(x => x.Date).DistinctBy(x => x.Date).ToList();

            if (listOfDates != null && listOfDates.Count() > 0)
            {
                foreach (var dt in listOfDates)
                {
                    var groupedItems = Details
                          .Where(item => item.Date == dt);
                    List<PaddyReportDetails> reportData = new List<PaddyReportDetails>();

                    if (groupedItems != null && groupedItems.Count() > 0)
                    {
                        reportData = groupedItems.ToList();
                         
                        PaddyReport reportPerDay = new PaddyReport();
                        reportPerDay.Details = new List<PaddyReportDetails>();
                        if (Details != null && Details.Count() > 0 && reportData != null && reportData.Count() > 0)
                        {
                            reportPerDay.Details.AddRange(reportData);

                            ReportData data = new ReportData();
                            data.Weight = reportData.Sum(x => x.Weight); data.Bags = reportData.Sum(x => x.Bags); data.ONB = reportData.Sum(x => x.ONB);
                            data.NB = reportData.Sum(x => x.NB); data.SS = reportData.Sum(x => x.SS); data.Count = reportData.Count();

                            reportPerDay.Total = data;
                            listOfReportPerDay.Add(reportPerDay);
                        }

                    }
                }
            }

            return listOfReportPerDay;
        }
    }
}
