using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.MasterData;
using TNCSC.Hulling.Repository.Interfaces;
using Grades = TNCSC.Hulling.Domain.MasterData.Grades;

namespace TNCSC.Hulling.Business.Services
{
    public class MasterDataService : IMasterDataService
    {
        protected IMasterDataRepository masterDataRepository;

        public MasterDataService(IMasterDataRepository masterDataRepository)
        {
            this.masterDataRepository = masterDataRepository;
        }

        public async Task<APIResponse> AddOrUpdateGunnyCondition(GunnyCondition gunnyCondition)
        {
            return await masterDataRepository.AddOrUpdateGunnyCondition(gunnyCondition);
        }

        public async Task<APIResponse> AddOrUpdateVarieryAndGrade(Grades variety)
        {
            return await masterDataRepository.AddOrUpdateVarieryAndGrade(variety);
        }

        public async Task<APIResponse> GetGunnyConditions()
        {
            return await masterDataRepository.GetGunnyConditions();
        }

        public async Task<APIResponse> GetVarietyandGrades()
        {
            return await masterDataRepository.GetVarietyandGrades();
        }

        public async Task<APIResponse> AddOrUpdateRegion(Region region)
        {
            return await masterDataRepository.AddOrUpdateRegion(region);
        }

        public async Task<APIResponse> GetAllRegion()
        {
            return await masterDataRepository.GetAllRegion();
        }

        public async Task<APIResponse> GetAllRegionById(int id)
        {
            return await masterDataRepository.GetAllRegionById(id);
        }

        public async Task<APIResponse> GetBillingReportDetails()
        {
            return await masterDataRepository.GetBillingReportDetails();
        }
    }
}
