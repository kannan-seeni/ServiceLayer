#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.FRK;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// FRKController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class FRKController : Controller
    {
        #region Declarations
        protected IFRKService frkService;
        #endregion

        #region Constructor
        public FRKController(IFRKService _frkService)
        {
            frkService = _frkService;
        }
        #endregion

        #region AddNewFRK

        [HttpPost(ApiRoutes.FRK.addNewFRK)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddNewFRK([FromBody] FRK frkObj)
        {
            var response = await frkService.AddFRKDetails(frkObj);

            return Ok(response);

        }

        #endregion

        #region GetAllFRKDetails

        [HttpGet(ApiRoutes.FRK.getAllFRKDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllFRKDetails()
        {
            var response = await frkService.GetAllDetails();

            return Ok(response);

        }
        #endregion

        #region GetFRKDetailsByMillId

        [HttpGet(ApiRoutes.FRK.getFRKByMillId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetFRKDetailsByMillId([FromRoute] long millId)
        {
            var response = await frkService.GetFRKDetailsByMillId(millId);

            return Ok(response);

        }

        #endregion

        #region GetFRKDetailsById

        [HttpGet(ApiRoutes.FRK.getFRKDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetFRKDetailsById([FromRoute] long id)
        {
            var response = await frkService.GetFRKDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region UpdateFRKDetails

        [HttpPut(ApiRoutes.FRK.updateFRKDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdateFRKDetails([FromBody] FRK frkObj)
        {
            var response = await frkService.UpdateFRKDetails(frkObj);

            return Ok(response);

        }
        #endregion
    }
}
