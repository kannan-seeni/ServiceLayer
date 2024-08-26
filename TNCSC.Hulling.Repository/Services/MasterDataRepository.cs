using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.MasterData;
using TNCSC.Hulling.Domain.Paddy;
using TNCSC.Hulling.Domain.Reports;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
using Grades = TNCSC.Hulling.Domain.MasterData.Grades;
using GunnyCondition = TNCSC.Hulling.Domain.MasterData.GunnyCondition;
using Variety = TNCSC.Hulling.Domain.MasterData.Variety;

namespace TNCSC.Hulling.Repository.Services
{
    public class MasterDataRepository : BaseRepository, IMasterDataRepository
    {
        public MasterDataRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
            : base(httpContentAccessor, objConfig, configManager, logger, _connection)
        {
        }

        public async Task<APIResponse> AddOrUpdateGunnyCondition(GunnyCondition gunnyCondition)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            try
            {
                if (gunnyCondition != null)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", gunnyCondition.Id, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@condition", gunnyCondition.Condition, DbType.String, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy ", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int32, ParameterDirection.Output);

                    var response = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddOrUpdateGunnyCondition", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int32>("@_id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.GCAddedOrUpdatedSuccessfully;
                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddOrUpdateGC;
                }


                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:AddOrUpdateGunnyCondition";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingGC;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingGC;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingOrUpdatingGC);
                return aPIResponse;
                throw;
            }
        }

        public async Task<APIResponse> GetGunnyConditions()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            try
            {
                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<GunnyCondition>((SqlConnection)connection, "SP_GetGunnyConditions", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    List<GunnyCondition> gc = response.ToList();
                    aPIResponse.data = gc;
                    aPIResponse.responseCode = ResponseCode.GCsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoGCsFound;
                }
                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:GetGunnyConditions";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetGCs;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetGCs;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetGCs);
                return aPIResponse;
                throw;
            }

        }
        public async Task<APIResponse> AddOrUpdateVarieryAndGrade(Domain.MasterData.Grades grade)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            try
            {
                if (grade != null)
                {
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@id", grade.Id, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@grade", grade.Grade, DbType.String, ParameterDirection.Input);
                    parameter.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameter.Add("@modifiedBy ", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameter.Add("@status", grade.Status, DbType.Boolean, ParameterDirection.Input);
                    parameter.Add("@_id", -1, DbType.Int32, ParameterDirection.Output);
                    var grades = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddOrUpdateGrade", parameter, commandType: CommandType.StoredProcedure);

                    var Id = parameter.Get<Int32>("@_id");

                    if (grade.Variety != null && grade.Variety.Count() > 0)
                    {
                        foreach (var item in grade.Variety)
                        {
                            DynamicParameters parameters = new DynamicParameters();

                            parameters.Add("@id", item.Id, DbType.Int32, ParameterDirection.Input);
                            parameters.Add("@variety", item.VarietyName, DbType.String, ParameterDirection.Input);
                            parameters.Add("@gradeId", Id, DbType.Int64, ParameterDirection.Input);
                            parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                            parameters.Add("@modifiedBy ", this.UserId, DbType.Int64, ParameterDirection.Input);
                            parameters.Add("@status", item.Status, DbType.Boolean, ParameterDirection.Input);
                            parameters.Add("@_id", -1, DbType.Int32, ParameterDirection.Output);

                            var response = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddOrUpdateVariety", parameters, commandType: CommandType.StoredProcedure);

                        }
                    }
                    aPIResponse.responseCode = ResponseCode.VarietyAddOrUpdatedSuccessfully;
                    aPIResponse.data = Id;

                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddOrUpdateVariety;
                }




                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:AddOrUpdateVarieryAndGrade";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingVariety;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingVariety;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingOrUpdatingVariety);
                return aPIResponse;
                throw;
            }
        }

        public async Task<APIResponse> GetVarietyandGrades()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            List<Grades> varieties = new List<Grades>();

            try
            {
                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryMultipleAsync((SqlConnection)connection, "SP_GetVarietyWithGrades", parameters, commandType: CommandType.StoredProcedure);

                List<Grades> grade = response.Read<Grades>().ToList();
                List<Variety> variety = response.Read<Variety>().ToList();
                if (variety != null && variety.Count() > 0)
                {
                    foreach (var gr in grade)
                    {
                        Grades data = new Grades();
                        List<Variety> vr = new List<Variety>();
                        data = gr;
                        var groupedItems = variety
                         .Where(item => item.GradeId == gr.Id);
                        if (groupedItems != null && groupedItems.Count() > 0)
                        {
                            vr = groupedItems.ToList();
                            data.Variety = new List<Variety>();
                            data.Variety.AddRange(vr);
                        }
                        varieties.Add(data);
                    }

                    aPIResponse.data = varieties;
                    aPIResponse.responseCode = ResponseCode.VarietyRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoVarietyFound;
                }
                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:GetVarietyandGrades";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetVariety;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetVariety;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetVariety);
                return aPIResponse;
                throw;
            }

        }

        public async Task<APIResponse> AddOrUpdateRegion(Region region)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            try
            {
                if (region != null)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@id", region.Id, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@region", region.RegionName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@frkPercentage", region.FRKPercentage, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy ", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@_id", -1, DbType.Int32, ParameterDirection.Output);

                    var response = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddOrUpdateRegion", parameters, commandType: CommandType.StoredProcedure);

                    var Id = parameters.Get<Int32>("@_id");

                    aPIResponse.data = Id;
                    aPIResponse.responseCode = ResponseCode.RegionAddedOrUpdatedSuccessfully;
                }
                else
                {
                    aPIResponse.data = 0;
                    aPIResponse.responseCode = ResponseCode.UnableToAddOrUpdateRegion;
                }


                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:AddOrUpdateRegion";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingRegion;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingRegion;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingOrUpdatingRegion);
                return aPIResponse;
                throw;
            }
        }

        public async Task<APIResponse> GetAllRegion()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            List<Region> regions = new List<Region>();

            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryAsync<Region>((SqlConnection)connection, "SP_GetAllRegion", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    regions = response.ToList();
                    aPIResponse.responseCode = ResponseCode.RegionDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoRegionFound;
                }

                aPIResponse.data = regions;

                var result = await GetBillingReportDetails();

                aPIResponse.data = result.data;
                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:GetAllRegion";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetRegionDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetRegionDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetRegionDetails);
                return aPIResponse;
                throw;
            }
        }

        public async Task<APIResponse> GetAllRegionById(int id)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            Region regions = new Region();

            try
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@id", id, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var response = await SqlMapper.QueryAsync<Region>((SqlConnection)connection, "SP_GetRegionById", parameters, commandType: CommandType.StoredProcedure);

                if (response != null && response.Count() > 0)
                {
                    regions = response.ToList().FirstOrDefault();
                    aPIResponse.responseCode = ResponseCode.RegionDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.NoRegionFound;
                }

                aPIResponse.data = regions;


                return aPIResponse;
            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "MasterDataRepository:GetAllRegionById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetRegionDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetRegionDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetRegionDetails);
                return aPIResponse;
                throw;
            }
        }



        public async Task<APIResponse> GetBillingReportDetails()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            List<PaddyBillingReport> report1 = new List<PaddyBillingReport>();
            List<RiceBillingReport> report2 = new List<RiceBillingReport>();
            List<BillingReport> report = new List<BillingReport>();

            try
            {

                DynamicParameters parameters = new DynamicParameters();

                var response = await SqlMapper.QueryMultipleAsync((SqlConnection)connection, "SP_Report_BillingReport", parameters, commandType: CommandType.StoredProcedure);

                report1 = response.Read<PaddyBillingReport>().ToList();
                report2 = response.Read<RiceBillingReport>().ToList();

                var out1 = report1.Select(x => x.OutTurn).ToList();


               // List<BillingPaddy> paddyReport = new List<BillingPaddy>();
                List<BillingPaddy> riceReport = new List<BillingPaddy>();


                List<BillingPaddy> paddyReport = report1
                           .GroupBy(x => x.OutTurn)
                           .Select((group, id) => new BillingPaddy
                           {
                               Id = id + 1,
                               OutTurn = group.Key,
                               TotalPaddyWeight = group.First().TotalPaddyWeight,
                               DueDate = group.First().DueDate,
                               Report = group.Select((item, j) => new PaddyReportForBill
                               {
                                   RowId = j + 1,
                                   RowNumber = item.RowNumber,
                                   Date = item.Date,
                                   IssueMemoNo = item.IssueMemoNo,
                                   PaddyWeight = item.PaddyWeight
                               }).ToList()
                           })
                           .ToList();

                out1 = out1.Distinct().ToList();
                if (out1 != null && out1.Count() > 0)
                {
                    int id = 0;
                    foreach (var it in out1)
                    {

                        id++;
                        decimal sum = 0;
                        BillingPaddy rBilling = new BillingPaddy
                        {
                            Report = new List<PaddyReportForBill>(),
                            Id = id
                        };

                        var reportofRice = new List<RiceBillingReport>();

                        foreach (var item2 in report2)
                        {
                            sum += item2.RiceWeight;

                            if (sum <= it)
                            {
                                reportofRice.Add(item2);
                                rBilling.Report.Add(new PaddyReportForBill
                                {
                                    RowId = rBilling.Report.Count + 1,
                                    RowNumber = item2.RowNumber,
                                    ADDate = item2.ADDate,
                                    ADNumber = item2.ADNumber,
                                    RiceWeight = item2.RiceWeight,
                                    TotalWeight = sum
                                });
                                rBilling.TotalRiceWeight = sum;
                            }
                            else if (sum > it && sum != it)
                            {

                                var adjustedWeight = it - (sum - item2.RiceWeight);
                                rBilling.Report.Add(new PaddyReportForBill
                                {
                                    RowId = rBilling.Report.Count + 1,
                                    RowNumber = item2.RowNumber,
                                    ADDate = item2.ADDate,
                                    ADNumber = item2.ADNumber,
                                    RiceWeight = adjustedWeight,
                                    TotalWeight = (sum - item2.RiceWeight) + adjustedWeight
                                });
                                rBilling.TotalRiceWeight = (sum - item2.RiceWeight) + adjustedWeight;
                                item2.RiceWeight -= adjustedWeight;
                                break;
                            }
                        }
                         
                        riceReport.Add(rBilling);
                        report2.RemoveAll(x => reportofRice.Any(y => x.RowNumber == y.RowNumber));

                    }

                }
 
                foreach (var item in paddyReport)
                {
                    var matchingRiceReports = riceReport
                        .Where(x => x.Id == item.Id)
                        .SelectMany(r => r.Report)
                        .ToDictionary(r => r.RowId);

                    foreach (var i in item.Report)
                    {
                        if (matchingRiceReports.TryGetValue(i.RowId, out var matchingRiceReport))
                        {
                            // Update report based on matchingRiceReport
                            i.ADNumber = matchingRiceReport.ADNumber;
                            i.ADDate = matchingRiceReport.ADDate;
                            i.RiceWeight = matchingRiceReport.RiceWeight;
                            i.TotalWeight = matchingRiceReport.TotalWeight;
                            item.TotalRiceWeight += matchingRiceReport.RiceWeight;
                        }
                        
                    }

                    // Set or ensure values for each item
                    item.DueDate = item.DueDate;
                    item.OutTurn = item.OutTurn;
                    item.TotalPaddyWeight = item.TotalPaddyWeight;
                }
                 
                aPIResponse.data = paddyReport;
                return aPIResponse;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
