#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.Godown;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// GodownController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class GodownController : Controller
    {
        #region Declarations

        protected IGodownService godownServices;
        #endregion

        #region Constructor

        public GodownController(IGodownService _godownServices)
        {
            godownServices = _godownServices;
        }
        #endregion

        #region AddNewGodown

        [HttpPost(ApiRoutes.Godown.addNewGodwon)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddNewGodown([FromBody] Godown godownObj)
        {
            var response = await godownServices.AddNewGodown(godownObj);

            return Ok(response);

        }

        #endregion

        #region GetAllGodowns

        [HttpGet(ApiRoutes.Godown.getAllGodwons)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllGodowns()
        {
            var response = await godownServices.GetAllGodowns();

            return Ok(response);

        }

        #endregion

        #region GetGodownDetailsByRegion

        [HttpGet(ApiRoutes.Godown.getGodwonByRegionId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGodownDetailsByRegion([FromRoute] int Id, [FromRoute] int typeId)
        {
            var response = await godownServices.GetGodownDetailsByRegion(Id, typeId);

            return Ok(response);

        }

        #endregion

        #region GetGodownDetailsById

        [HttpGet(ApiRoutes.Godown.getGodwonDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGodownDetailsById([FromRoute] long id)
        {
            var response = await godownServices.GetGodownDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region UpdateGodwonDetails

        [HttpPut(ApiRoutes.Godown.updateGodwonDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdateGodwonDetails([FromBody] Godown godownObj)
        {
            var response = await godownServices.UpdateGodownDetails(godownObj);

            return Ok(response);

        }
        #endregion

        #region ActiveOrInActivateGodown

        [HttpPut(ApiRoutes.Godown.activeOrInactiveGodown)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> ActiveOrInActivateGodown([FromRoute]long godownId,bool status)
        {
            var response = await godownServices.ActiveOrInActivateGodown(godownId, status);

            return Ok(response);

        }
        #endregion
    }
}
