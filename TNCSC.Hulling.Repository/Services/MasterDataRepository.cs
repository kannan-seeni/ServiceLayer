using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Domain;
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

                    var Id = parameters.Get<Int32>("@id");

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



        //public async Task<APIResponse> AddOrUpdateGrade(List<Grades> grades)
        //{
        //    APIResponse aPIResponse = new APIResponse();
        //    aPIResponse.version = sVersion;

        //    try
        //    {
        //        if (grades != null)
        //        {
                     
        //                foreach (var item in grades)
        //                {
        //                    DynamicParameters parameter = new DynamicParameters();

        //                    parameter.Add("@id", item.Id, DbType.Int32, ParameterDirection.Input);
        //                    parameter.Add("@grade", item.Grade, DbType.String, ParameterDirection.Input);
        //                    parameter.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
        //                    parameter.Add("@modifiedBy ", this.UserId, DbType.Int64, ParameterDirection.Input);
        //                    parameter.Add("@status", item.Status, DbType.Boolean, ParameterDirection.Input);
        //                    parameter.Add("@varietyId", item.VarietyId, DbType.Int64, ParameterDirection.Input);

        //                    var grade = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_AddOrUpdateGrade", parameter, commandType: CommandType.StoredProcedure);
        //                }
                    
        //            aPIResponse.responseCode = ResponseCode.VarietyAddOrUpdatedSuccessfully;
        //            aPIResponse.data = 1;

        //        }
        //        else
        //        {
        //            aPIResponse.data = 0;
        //            aPIResponse.responseCode = ResponseCode.UnableToAddOrUpdateVariety;
        //        }




        //        return aPIResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerModel logmodel = new LoggerModel();
        //        logmodel.AdditionalInfo = "MasterDataRepository:AddOrUpdateGrade";
        //        logmodel.ExecptionDetails = ex;
        //        logmodel.ResponseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingVariety;
        //        logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
        //        _Logger.Log(LogType.Error, logmodel);
        //        aPIResponse.responseCode = ResponseCode.ExceptionOccursInAddingOrUpdatingVariety;
        //        aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInAddingOrUpdatingVariety);
        //        return aPIResponse;
        //        throw;
        //    }
        //}

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
                    foreach(var gr in grade)
                    {
                        Grades data = new Grades();
                        List<Variety> vr = new List<Variety>();
                        data = gr;
                        var groupedItems = variety
                         .Where(item => item.GradeId == gr.Id);
                        if(groupedItems != null && groupedItems.Count() > 0)
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
    }
}
