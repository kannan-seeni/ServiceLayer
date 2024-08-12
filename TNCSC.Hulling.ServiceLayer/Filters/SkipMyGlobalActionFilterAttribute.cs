#region References
using Microsoft.AspNetCore.Mvc.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Filters
{
    /// <summary>
    /// SkipMyGlobalActionFilterAttribute
    /// </summary>
    public class SkipMyGlobalActionFilterAttribute : Attribute, IFilterMetadata
    {
    }
}
