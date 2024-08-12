using Dapper;
#region References
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;
using TNCSC.Hulling.Components.Models;
using TNCSC.Hulling.Components.Options;
#endregion

namespace TNCSC.Hulling.Components.Filters
{
    /// <summary>
    /// AuditAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuditAttribute : ActionFilterAttribute
    {
        #region Declarations
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AuditAttributeOptions _auditAttributeOptions;

        private bool bIsAuditEnabled = false;

        private bool bIsPayLoadEnabled = false;

        private string sConnectionString;
        #endregion

        #region Constructor
        public AuditAttribute(IHttpContextAccessor httpContextAccessor, IOptions<AuditAttributeOptions> options)
        {

            _httpContextAccessor = httpContextAccessor;
            _auditAttributeOptions = options.Value;
            sConnectionString = _auditAttributeOptions.ConnectionString;
            bIsAuditEnabled = _auditAttributeOptions.IsAuditEnabled;
            bIsPayLoadEnabled = _auditAttributeOptions.IsPayloadRecorded;
        }
        #endregion

        #region OnActionExecuting
        public override void OnActionExecuting(ActionExecutingContext FilterContext)
        {
            if (bIsAuditEnabled)
            {
                AuditModel oAudit = BuildAuditModel(FilterContext, 0);
                if (oAudit != null)
                {
                    if (!string.IsNullOrEmpty(sConnectionString))
                        InsertAuditLogs(oAudit, sConnectionString);
                }
            }
            base.OnActionExecuting(FilterContext);

        }
        #endregion

        #region OnActionExecuted
        public override void OnActionExecuted(ActionExecutedContext FilterContext)
        {
            if (bIsAuditEnabled)
            {
                AuditModel oAudit = BuildAuditModel(FilterContext, 1);
                if (oAudit != null)
                {
                    if (!string.IsNullOrEmpty(sConnectionString) && bIsAuditEnabled)
                         InsertAuditLogs(oAudit, sConnectionString);
                }
            }
            base.OnActionExecuted(FilterContext);
        }
        #endregion

        #region OnResultExecuted
        public override void OnResultExecuted(ResultExecutedContext FilterContext)
        {

            base.OnResultExecuted(FilterContext);
        }
        #endregion

        #region OnResultExecuting
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        #endregion

        #region BuildAuditModel
        private AuditModel BuildAuditModel(dynamic filterContext, int sType)
        {
            try
            {
                if (filterContext == null || filterContext.HttpContext == null)
                    return null;

                //Stores the Request in an Accessible object
                var request = filterContext.HttpContext.Request;

                if (request == null)
                    return null;


                var controllerName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ControllerName;
                var actionName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ActionName;

                AuditModel objaudit = new();

                IHeaderDictionary headersDictionary = request.Headers;

                if (!string.IsNullOrEmpty(headersDictionary[HeaderNames.Referer].ToString()))
                    objaudit.URLReferrer = headersDictionary[HeaderNames.Referer].ToString();

                //objaudit.AuditID = Guid.NewGuid();
                objaudit.TimeAccessed = DateTime.UtcNow;

                //stores the payload , it will consider the first object as payload
                if (bIsPayLoadEnabled && sType == 0)
                {
                    object entity = new object();

                    foreach (ControllerParameterDescriptor param in filterContext.ActionDescriptor.Parameters)
                    {
                        if (filterContext.ActionArguments.ContainsKey(param.Name))
                        {
                            entity = filterContext.ActionArguments[param.Name];
                        }
                    }

                    byte[] utf8bytesJson = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(entity);
                    string strResult = System.Text.Encoding.UTF8.GetString(utf8bytesJson);
                    objaudit.PayLoad = strResult;
                }

                objaudit.Action = GetAuditType(sType);

                objaudit.IPAddress = filterContext.HttpContext.Request.Host.ToString();
                objaudit.MethodName = Convert.ToString(filterContext.HttpContext.Request.Path); // URL User Requested 
                objaudit.ControllerName = controllerName; // ControllerName 
                objaudit.ActionName = actionName;
                objaudit.UserID = (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) ? _httpContextAccessor.HttpContext.User.Claims.Where(p => p.Type == "Id").FirstOrDefault().Value : "0";
 
                objaudit.UserName = (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) ? _httpContextAccessor.HttpContext.User.Claims.Where(p => p.Type == "name").FirstOrDefault().Value : "";
                objaudit.LoginStatus = (filterContext.HttpContext.User.Identity.IsAuthenticated) ? "A" : "D";
                 
                if ( _httpContextAccessor.HttpContext != null)
                    objaudit.IPAddress = Convert.ToString(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress);

                objaudit.CreatedBy = objaudit.UserName;
                objaudit.CreatedDate = DateTime.UtcNow;

                return objaudit;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetAuditType
        public string GetAuditType(int enumID)
        {
            switch (enumID)
            {
                case 0:
                    return "Entry";
                case 1:
                    return "Exit";
                case 2:
                    return "Get";
                case 3:
                    return "Update";
                case 4:
                    return "Delete";
                default:
                    return "Others";
            }
        }
        #endregion

        #region InsertAuditLogs
        public void InsertAuditLogs(AuditModel objauditmodel, string sConnectionString)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@MethodName", !string.IsNullOrEmpty(objauditmodel.MethodName) ? objauditmodel.MethodName : "");
                parameters.Add("@ControllerName", !string.IsNullOrEmpty(objauditmodel.ControllerName) ? objauditmodel.ControllerName : "");
                parameters.Add("@ActionName", !string.IsNullOrEmpty(objauditmodel.ActionName) ? objauditmodel.ActionName : "");
                parameters.Add("@Action", !string.IsNullOrEmpty(objauditmodel.Action) ? objauditmodel.Action : "");
                parameters.Add("@URLReferrer", !string.IsNullOrEmpty(objauditmodel.URLReferrer) ? objauditmodel.URLReferrer : "");
                parameters.Add("@IPAddress", !string.IsNullOrEmpty(objauditmodel.IPAddress) ? objauditmodel.IPAddress : "");
                parameters.Add("@SessionID", !string.IsNullOrEmpty(objauditmodel.@SessionID) ? objauditmodel.@SessionID : "");
                parameters.Add("@UserID", !string.IsNullOrEmpty(objauditmodel.UserID) ? objauditmodel.UserID : "");
                parameters.Add("@UserName", !string.IsNullOrEmpty(objauditmodel.UserName) ? Convert.ToString(objauditmodel.UserName) : "");
                parameters.Add("@TimeAccessed", !string.IsNullOrEmpty(Convert.ToString(objauditmodel.TimeAccessed)) ? Convert.ToString(objauditmodel.TimeAccessed) : "");
                parameters.Add("@LoginStatus", !string.IsNullOrEmpty(objauditmodel.LoginStatus) ? objauditmodel.LoginStatus : "");
                parameters.Add("@LoggedInAt", !string.IsNullOrEmpty(objauditmodel.LoggedInAt) ? objauditmodel.LoggedInAt : "");
                parameters.Add("@LoggedOutAt", !string.IsNullOrEmpty(objauditmodel.LoggedOutAt) ? objauditmodel.LoggedOutAt : "");
                parameters.Add("@PayLoad", !string.IsNullOrEmpty(Convert.ToString(objauditmodel.PayLoad)) ? Convert.ToString(objauditmodel.PayLoad) : "");
                parameters.Add("@CreatedBy", !string.IsNullOrEmpty(objauditmodel.CreatedBy) ? objauditmodel.CreatedBy : "");
                parameters.Add("@CreatedDate", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));

                using (var con = new SqlConnection(sConnectionString))
                {
                    con.Open();
                    con.Execute("USP_InsertAuditLogs", parameters, null, 0, CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
    }
}
