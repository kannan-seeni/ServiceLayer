#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.Rice;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// RiceController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class RiceController : Controller
    {
        #region Declarations

        protected IRiceService riceService;

        #endregion

        #region Constructor
        public RiceController(IRiceService _riceService)
        {
            riceService = _riceService;
        }
        #endregion

        #region AddRiceDetails

        [HttpPost(ApiRoutes.Rice.addNewRice)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddRiceDetails([FromBody] Rice riceObj)
        {
            var response = await riceService.AddRiceDetails(riceObj);

            return Ok(response);

        }

        #endregion

        #region GetAllRiceDetails

        [HttpGet(ApiRoutes.Rice.getAllRiceDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllRiceDetails()
        {
            var response = await riceService.GetAllDetails();

            return Ok(response);

        }

        #endregion

        #region GetPaddyDetailsByMillId

        [HttpGet(ApiRoutes.Rice.getRiceByMillId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetPaddyDetailsByMillId([FromRoute]long millId)
        {
            var response = await riceService.GetRiceDetailsByMillId(millId);

            return Ok(response);

        }

        #endregion

        #region GetRiceDetailsById

        [HttpGet(ApiRoutes.Rice.getRiceDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetRiceDetailsById([FromRoute]long id)
        {
            var response = await riceService.GetRiceDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region UpdateRiceDetails

        [HttpPut(ApiRoutes.Rice.updateRiceDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdateRiceDetails([FromBody] Rice riceObj)
        {
            var response = await riceService.UpdateRiceDetails(riceObj);

            return Ok(response);

        }
        #endregion


        [HttpGet(ApiRoutes.Rice.monthlyReport)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> RiceMonthReport([FromRoute] long millId, [FromRoute] string month)
        {
            var response = await riceService.RiceMonthReport(millId,month);

            return Ok(response);

        }
    }
}