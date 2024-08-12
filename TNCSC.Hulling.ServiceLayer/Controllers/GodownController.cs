#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.Godwon;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// GodwonController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class GodownController : Controller
    {
        #region Declarations

        protected IGodwonService godwonServices;
        #endregion

        #region Constructor

        public GodownController(IGodwonService _godwonServices)
        {
            godwonServices = _godwonServices;
        }
        #endregion

        #region AddNewGodwon

        [HttpPost(ApiRoutes.Godown.addNewGodwon)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddNewGodwon([FromBody] Godwon godwonObj)
        {
            var response = await godwonServices.AddNewGodwon(godwonObj);

            return Ok(response);

        }

        #endregion

        #region GetAllGodwons

        [HttpGet(ApiRoutes.Godown.getAllGodwons)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllGodwons()
        {
            var response = await godwonServices.GetAllGodwons();

            return Ok(response);

        }

        #endregion

        #region GetGodwonByMillId

        [HttpGet(ApiRoutes.Godown.getGodwonByMillId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGodwonByMillId([FromRoute] long millId, [FromRoute] int typeId)
        {
            var response = await godwonServices.GetGodwonDetailsByMillId(millId, typeId);

            return Ok(response);

        }

        #endregion

        #region GetGodwonById

        [HttpGet(ApiRoutes.Godown.getGodwonDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGodwonById([FromRoute] long id)
        {
            var response = await godwonServices.GetGodwonDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region GetGodwonById

        [HttpPut(ApiRoutes.Godown.updateGodwonDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdateGodwonDetails([FromBody] Godwon godwonObj)
        {
            var response = await godwonServices.UpdateGodwonDetails(godwonObj);

            return Ok(response);

        }
        #endregion
    }
}
