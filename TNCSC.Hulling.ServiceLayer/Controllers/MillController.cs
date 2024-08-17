using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.Mill;
using TNCSC.Hulling.ServiceLayer.Filters;

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    [SkipMyGlobalActionFilter]
    public class MillController : Controller
    {
        #region Declarations
        protected IMillService millService;
        #endregion

        #region Constructor
        public MillController(IMillService _millService)
        {
            millService = _millService;
        }
        #endregion

        #region AddNewMill

        [HttpPost(ApiRoutes.Mill.addNewmill)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddNewMill([FromBody] Mill millObj)
        {
            var response = await millService.AddNewMill(millObj);

            return Ok(response);

        }

        #endregion

        #region GetAllMills

        [HttpGet(ApiRoutes.Mill.getAllMillDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllMills()
        {
            var response = await millService.GetAllMills();

            return Ok(response);

        }
        #endregion

        #region GetMillDetailsByMillId

        [HttpGet(ApiRoutes.Mill.getMillDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetMillDetailsByMillId([FromRoute] long id)
        {
            var response = await millService.GetMillDetailsByMillId(id);

            return Ok(response);

        }

        #endregion

        #region UpdateMillDetails

        [HttpPut(ApiRoutes.Mill.updateMillDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdateMillDetails([FromBody] Mill millObj)
        {
            var response = await millService.UpdateMillDetails(millObj);

            return Ok(response);

        }
        #endregion

        #region ActiveOrInActivateMill

        [HttpPut(ApiRoutes.Mill.activeOrInactiveMill)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> ActiveOrInActivateMill([FromRoute] long millId, [FromRoute] bool status)
        {
            var response = await millService.ActiveOrInActivateMill(millId, status);

            return Ok(response);

        }
        #endregion
    }
}
