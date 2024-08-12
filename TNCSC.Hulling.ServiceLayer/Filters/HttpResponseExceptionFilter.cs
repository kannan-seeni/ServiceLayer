#region References
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Filters
{
    /// <summary>
    /// HttpResponseExceptionFilter
    /// </summary>
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {

        #region Order
        public int Order { get; set; } = int.MaxValue - 10;
        #endregion

        #region OnActionExecuting
        public void OnActionExecuting(ActionExecutingContext context) { }
        #endregion

        #region OnActionExecuted
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;
            }
        }
        #endregion

    }
}
