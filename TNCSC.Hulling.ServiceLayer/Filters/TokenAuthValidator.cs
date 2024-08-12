#region References
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Repository.Helpers;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Filters
{
    /// <summary>
    /// TokenAuthValidator
    /// </summary>
    public class TokenAuthValidator : Attribute, IAsyncActionFilter
    {
        private const string sVersion = "v1.0.0.1";

        #region OnActionExecutionAsync
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;
            if (!context.Filters.OfType<SkipMyGlobalActionFilterAttribute>().Any())
            {
                try
                {
                    if (context.HttpContext.User.Claims.Count() > 0)
                    {
                       
                        if (Convert.ToInt32(context.HttpContext.User?.Claims?.Where(p => p.Type == "Id").FirstOrDefault()?.Value) == 0)
                        {
                            var loginSuccessResponse = "Unauthorized access please contact the administrator";
                            aPIResponse.data = loginSuccessResponse;
                            aPIResponse.responseCode = ResponseCode.InvalidUserToken;
                            context.Result = new BadRequestObjectResult(aPIResponse);
                            return;
                        }
                    }
                    else
                    {
                        var loginSuccessResponse = "Unauthorized access please contact the administrator";
                        aPIResponse.data = loginSuccessResponse;
                        aPIResponse.responseCode = ResponseCode.InvalidUserToken;
                        context.Result = new BadRequestObjectResult(aPIResponse);
                        return;

                    }

                }
                catch (Exception)
                {
                    var loginSuccessResponse = "Unauthorized access please contact the administrator";
                    aPIResponse.data = loginSuccessResponse;
                    aPIResponse.responseCode = ResponseCode.InvalidUserToken;
                    context.Result = new BadRequestObjectResult(aPIResponse);
                    return;

                }
            }
            await next();
        }

        #endregion
    }
}
