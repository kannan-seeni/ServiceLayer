using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.MasterData;
using TNCSC.Hulling.ServiceLayer.Filters;

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    [SkipMyGlobalActionFilter]
    public class MasterDataController : Controller
    {
        protected IMasterDataService masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
            this.masterDataService = masterDataService;
        }

        #region AddOrUpdateGunnyCondition

        [HttpPost(ApiRoutes.MasterData.gunnyCondition)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddOrUpdateGunnyCondition([FromBody] GunnyCondition gunnyCondition)
        {
            var response = await masterDataService.AddOrUpdateGunnyCondition(gunnyCondition);

            return Ok(response);

        }

        #endregion

        #region GetGunnyConditions

        [HttpGet(ApiRoutes.MasterData.getAllGCDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetGunnyConditions()
        {
            var response = await masterDataService.GetGunnyConditions();

            return Ok(response);

        }
        #endregion

        #region AddOrUpdateVarieryAndGrade

        [HttpPost(ApiRoutes.MasterData.addVariety)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddOrUpdateVarieryAndGrade([FromBody] Grades variety)
        {
            var response = await masterDataService.AddOrUpdateVarieryAndGrade(variety);

            return Ok(response);

        }

        #endregion

        #region GetVarietyandGrades

        [HttpGet(ApiRoutes.MasterData.getAllVarietyDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetVarietyandGrades()
        {
            var response = await masterDataService.GetVarietyandGrades();

            return Ok(response);

        }
        #endregion

        #region AddOrUpdateRegion

        [HttpPost(ApiRoutes.MasterData.addRegion)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> AddOrUpdateRegion([FromBody] Region region)
        {
            var response = await masterDataService.AddOrUpdateRegion(region);

            return Ok(response);

        }

        #endregion

        #region GetAllRegion

        [HttpGet(ApiRoutes.MasterData.getAllRegionDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllRegion()
        {
            var response = await masterDataService.GetAllRegion();

            return Ok(response);

        }
        #endregion

        #region GetAllRegionById

        [HttpGet(ApiRoutes.MasterData.getAllRegionDetailsById)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllRegionById([FromRoute] int id)
        {
            var response = await masterDataService.GetAllRegionById(id);

            return Ok(response);

        }
        #endregion
    }
}
