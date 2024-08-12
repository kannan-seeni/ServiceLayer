#region References
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components.Interfaces;
#endregion

namespace TNCSC.Hulling.Repository.Services
{
    /// <summary>
    /// BaseRepository
    /// </summary>
    public class BaseRepository : IDisposable
    {
        #region Declarations
        protected IDbConnection connection;

        protected SqlConnectionStringBuilder dynamicConnectionBuilder;

        protected IConfigManager _configManager;

        protected int UserId = 0;

        private HttpContext httpContext;

        protected ILogger _Logger;

        public string sVersion;
        #endregion

        #region Constructor

        /// <summary>
        /// BaseRepository Constructor
        /// </summary>
        /// <param name="httpContentAccessor"></param>
        /// <param name="objConfig"></param>
        public BaseRepository(IHttpContextAccessor httpContentAccessor, IConfiguration objConfig, IConfigManager configManager, ILogger logger, IDbConnection _connection)
        {
            _Logger = logger;
            _configManager = configManager;
            httpContext = httpContentAccessor.HttpContext;
            connection = _connection;
            sVersion = "v1.0.0.1";

            if (configManager != null)
            {
                UserId = this.GetUserID();

            }
        }

        #endregion

        #region GetUserID

        /// <summary>
        /// GetUserId
        /// </summary>
        /// <returns></returns>
        private int GetUserID()
        {
            try
            {

                var IsAvailable = this.httpContext.User.Claims.First(p => p.Type == "Id");
                bool success = int.TryParse(this.httpContext.User?.Claims.Where(p => p.Type == "Id").FirstOrDefault().Value, out int _UserId);
                if (success)
                {

                    return _UserId;
                }
                else
                {

                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            //throw new NotImplementedException();
            httpContext = null;
        }
        #endregion
    }
}
