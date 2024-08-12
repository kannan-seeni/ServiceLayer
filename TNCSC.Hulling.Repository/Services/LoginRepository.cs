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
    /// LoginRepository
    /// </summary>
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        #region Constructor
        public LoginRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection connection)
            : base(httpContentAccessor, objConfig, configManager, logger, connection) 
        { 
        }
        #endregion

        #region ValidateUser
        public async Task<User> ValidateUser(string userId, string password)
        {
            User user = new User();
            try
            { 
                
                int userExists;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserID", !string.IsNullOrEmpty(userId) ? userId : "");
                parameters.Add("@Password", !string.IsNullOrEmpty(password) ? password : "");
                parameters.Add("@IsExists", -1, DbType.Int32, ParameterDirection.Output);

                var _user = await SqlMapper.QueryAsync<User>((SqlConnection)connection, "SP_Login", parameters, commandType: CommandType.StoredProcedure);
                userExists = parameters.Get<int>("@IsExists");
                if(_user != null)
                {
                    user = _user.ToList().FirstOrDefault();

                }

                if (userExists != 1)
                {
                    return null;
                }
                else
                {
                    return user;
                }
                

            }
            catch (Exception ex)
            {
                LoggerModel logmodel = new LoggerModel();
                logmodel.AdditionalInfo = "LoginRepository:ValidateUser";
                logmodel.ExecptionDetails = ex;
                logmodel.ResponseCode = ResponseCode.LoginException;
                logmodel.CreatedBy = this.UserId.ToString(); logmodel.UserID = this.UserId.ToString();
                _Logger.Log(LogType.Error, logmodel);
               
                throw;
            }
        }
        #endregion
    }
}
