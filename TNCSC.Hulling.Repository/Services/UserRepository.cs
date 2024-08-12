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
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// UserRepository
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        #region Declarations
        private string sVersion = "v1.0.0.1";
        #endregion

        #region Constructor
        public UserRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection connection)
            : base(httpContentAccessor, objConfig, configManager, logger, connection)
        {
             
        }
        #endregion

        #region Methods

        #region CreateUser
        public async Task<APIResponse> CreateUser(User userObj)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            try
            {
             
                if(userObj != null)
                { 
                     
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@password", userObj.Password, DbType.String, ParameterDirection.Input);
                    parameters.Add("@firstName", userObj.FirstName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@lastName", userObj.LastName, DbType.String, ParameterDirection.Input); 
                    parameters.Add("@millId", userObj.MillRefId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@emailId", userObj.EmailId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@mobileNo", userObj.MobileNo, DbType.String, ParameterDirection.Input);
                    parameters.Add("@roleId", userObj.Role, DbType.String, ParameterDirection.Input); 
                    parameters.Add("@createdBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@modifiedBy", this.UserId, DbType.Int64, ParameterDirection.Input);
                    parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@id", -1, DbType.Int64, ParameterDirection.Output);
                    parameters.Add("@userId", "", DbType.String, ParameterDirection.Output);

                    var id = await SqlMapper.ExecuteAsync((SqlConnection)connection, "SP_CreateUser", parameters, commandType: CommandType.StoredProcedure);
                    
                    var Id = parameters.Get<Int64>("@id");
                    var userID = parameters.Get<string>("@userId");
                    IDictionary<string, string> response = new Dictionary<string, string>();
                    if (Id != 0 && !string.IsNullOrEmpty(userID))
                    {
                        response.Add("id", Id.ToString());
                        response.Add("userId", userID);
                    }
                    aPIResponse.data = response;
                    aPIResponse.responseCode = ResponseCode.UserCreatedSuccessfully;
                    return aPIResponse;
                }
                else
                {
                    aPIResponse.responseCode = ResponseCode.UnableToCreateUser;
                    return aPIResponse;
                }
                

            }
            catch (Exception ex) 
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "UserRepository:CreateUser";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInUserCreation;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInUserCreation;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInUserCreation);
                return aPIResponse;
                throw;
            }


        }

        #endregion

        #region GetUsersByMillId
        public async Task<APIResponse> GetUsersByMillId(long millId)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            List<User> userDetails = new List<User>();
            try
            { 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@millrefId", millId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

               var  user = await SqlMapper.QueryAsync<User>((SqlConnection)connection, "SP_GetUsersByMillId", parameters, commandType: CommandType.StoredProcedure);

                if (user != null && user.Count() > 0)
                {
                    userDetails= user.ToList();
                    aPIResponse.data = userDetails;
                    aPIResponse.responseCode = ResponseCode.UserDetailsRetrivedSuccessfully;
                }
                else
                {
                    aPIResponse.data = userDetails;
                    aPIResponse.responseCode = ResponseCode.NoUsersFound;
                } 
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "UserRepository:GetUsersByMillId";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetUserDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetUserDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetUserDetails);
                return aPIResponse;
                throw;
            }


        }
        #endregion

        #region GetUserDetailsById
        public async Task<APIResponse> GetUserDetailsById(long id)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            User userDetails = new User();

            try
            { 
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@userId", id, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@status", true, DbType.Boolean, ParameterDirection.Input);

                var user = await SqlMapper.QueryFirstOrDefaultAsync<User>((SqlConnection)connection, "SP_GetUserDetailsById", parameters, commandType: CommandType.StoredProcedure);

                if (user != null)
                {
                    userDetails = user; 
                    aPIResponse.responseCode = ResponseCode.UserDetailsRetrivedSuccessfully;
                }
                else
                {
                    
                    aPIResponse.responseCode = ResponseCode.NoUsersFound;
                }
                aPIResponse.data = userDetails;
                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "UserRepository:GetUserDetailsById";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetUserDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetUserDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetUserDetails);
                return aPIResponse;
                throw;
            }


        }
        #endregion

        #region GetAllUser
        public async Task<APIResponse> GetAllUser()
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            List<User> userDetails = new List<User>();

            try
            {
                
                DynamicParameters parameters = new DynamicParameters();
                
                var user = await SqlMapper.QueryAsync<User>((SqlConnection)connection, "SP_GetAllUsers", parameters, commandType: CommandType.StoredProcedure);

                if (user != null && user.Count() > 0)
                {
                    userDetails = user.ToList(); 
                    aPIResponse.responseCode = ResponseCode.UserDetailsRetrivedSuccessfully;
                }
                else
                { 
                    aPIResponse.responseCode = ResponseCode.NoUsersFound;
                }
                aPIResponse.data = userDetails;

                return aPIResponse;

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "UserRepository:GetAllUser";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.ExceptionOccursInGetUserDetails;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
                aPIResponse.responseCode = ResponseCode.ExceptionOccursInGetUserDetails;
                aPIResponse.error = new ErrorModel(ex.Message, ResponseCode.ExceptionOccursInGetUserDetails);
                return aPIResponse;
                throw;
            }


        }
        #endregion

        #endregion
    }
}
