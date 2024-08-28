using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain.MasterData;
using TNCSC.Hulling.Domain.Reports;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.ServiceLayer.Export;
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


        #region GetAllRegion

        [HttpPost(ApiRoutes.MasterData.GetBillingReportDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetBillingReportDetails([FromBody]BillingReportRequest reportRequest)
        {
            var response = await masterDataService.GetBillingReportDetails(reportRequest);

            if(response.data != null)
            {
                PrintBillingReport printBillingReport = new PrintBillingReport();
                var s = printBillingReport.DownloadPDF(response.data, reportRequest.Reportmonth, reportRequest.Grade);
                AttachmentModel attachment = new AttachmentModel();

                attachment.FileContents = s;
                attachment.FileType = "application/pdf";
                attachment.FileName = "Billing Report" + "_" + reportRequest.Reportmonth + " (" + (reportRequest.Grade == "ADT" ? "A" : reportRequest.Grade) + "-Grade" + ")" + ".pdf";

                return File(attachment.FileContents, attachment.FileType, attachment.FileName);
            }
            else
            {
                if(response.responseCode == ResponseCode.PreviousMonthReportNotGenerated)
                {
                    response.data = "You didn't Generated the privious month report";
                    return Ok(response);
                }else if(response.responseCode == ResponseCode.NoTransactionBasedOnThisMonth)
                {
                    response.data = "No transaction found for this month";
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
                
            }
 

        }
        #endregion
    }
}
