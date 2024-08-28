using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.MasterData;
using TNCSC.Hulling.Domain.Reports;

namespace TNCSC.Hulling.Business.Interfaces
{
    public interface IMasterDataService
    {
        public Task<APIResponse> AddOrUpdateGunnyCondition(GunnyCondition gunnyCondition);
        public Task<APIResponse> GetGunnyConditions();
        public Task<APIResponse> AddOrUpdateVarieryAndGrade(Grades variety);
        public Task<APIResponse> GetVarietyandGrades();
        public Task<APIResponse> AddOrUpdateRegion(Region region);
        public Task<APIResponse> GetAllRegion();
        public Task<APIResponse> GetAllRegionById(int id);

        public Task<APIResponse> GetBillingReportDetails(BillingReportRequest reportRequest);
    }
}
