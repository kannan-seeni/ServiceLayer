using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.MasterData;
using TNCSC.Hulling.Repository.Interfaces;
using Variety = TNCSC.Hulling.Domain.MasterData.Variety;

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

        public async Task<APIResponse> AddOrUpdateVarieryAndGrade(Variety variety)
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
    }
}
