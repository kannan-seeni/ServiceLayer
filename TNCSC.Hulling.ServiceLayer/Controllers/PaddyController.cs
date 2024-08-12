#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.Paddy;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// PaddyController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class PaddyController : Controller
    {
        #region Declararions
        protected IPaddyService paddyService;
        #endregion

        #region Constructor
        public PaddyController(IPaddyService _paddyService)
        {
            paddyService = _paddyService;
        }
        #endregion

        #region AddPaddyDetails

        [HttpPost(ApiRoutes.Paddy.addNewPaddy)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddPaddyDetails([FromBody] Paddy paddyObj)
        {
            var response = await paddyService.AddPaddyDetails(paddyObj);

            return Ok(response);

        }

        #endregion

        #region GetAllPaddyDetails

        [HttpGet(ApiRoutes.Paddy.getAllPaddyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllPaddyDetails()
        {
            var response = await paddyService.GetAllDetails();

            return Ok(response);

        }

        #endregion

        #region GetPaddyDetailsByMillId

        [HttpGet(ApiRoutes.Paddy.getPaddyByMillId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetPaddyDetailsByMillId([FromRoute] long millId)
        {
            var response = await paddyService.GetPaddyDetailsByMillId(millId);

            return Ok(response);

        }

        #endregion

        #region GetPaddyDetailsById

        [HttpGet(ApiRoutes.Paddy.getPaddyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetPaddyDetailsById([FromRoute] long id)
        {
            var response = await paddyService.GetPaddyDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region UpdatePaddyDetails

        [HttpPut(ApiRoutes.Paddy.updatePaddyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdatePaddyDetails([FromBody] Paddy paddyObj)
        {
            var response = await paddyService.UpdatePaddyDetails(paddyObj);

            return Ok(response);

        }
        #endregion


        [HttpGet(ApiRoutes.Paddy.monthlyReport)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetPaddyDetailsByMillId([FromRoute] long millId, [FromRoute] string month)
        {
            var response = await paddyService.PaddyMonthlyReport(millId,month);

            return Ok(response);

        }
    }
}
