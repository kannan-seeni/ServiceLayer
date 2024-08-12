#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.Gunnys;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// GunnyController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class GunnyController : Controller
    {
        #region Declarations

        protected IGunnyService gunnyService;

        #endregion

        #region Constructor
        public GunnyController(IGunnyService _gunnyService)
        {
            gunnyService = _gunnyService;
        }

        #endregion

        #region AddNewGunny

        [HttpPost(ApiRoutes.Gunnys.addNewGunny)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddNewGunny([FromBody] Gunny gunnyObj)
        {
            var response = await gunnyService.AddGunnyDetails(gunnyObj);

            return Ok(response);

        }

        #endregion

        #region GetAllGunnyDetails

        [HttpGet(ApiRoutes.Gunnys.getAllGunnyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllGunnyDetails()
        {
            var response = await gunnyService.GetAllDetails();

            return Ok(response);

        }

        #endregion

        #region GetGunnyDetailsByMillId

        [HttpGet(ApiRoutes.Gunnys.getGunnyByMillId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGunnyDetailsByMillId([FromRoute] long millId)
        {
            var response = await gunnyService.GetGunnyDetailsByMillId(millId);

            return Ok(response);

        }

        #endregion

        #region GetGunnyDetailsById

        [HttpGet(ApiRoutes.Gunnys.getGunnyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGunnyDetailsById([FromRoute] long id)
        {
            var response = await gunnyService.GetGunnyDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region UpdateGunnyDetails

        [HttpPut(ApiRoutes.Gunnys.updateGunnyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> UpdateGunnyDetails([FromBody] Gunny gunnyObj)
        {
            var response = await gunnyService.UpdateGunnyDetails(gunnyObj);

            return Ok(response);

        }

        #endregion
    }
}
