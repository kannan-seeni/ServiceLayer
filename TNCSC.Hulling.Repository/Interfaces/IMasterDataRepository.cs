using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.MasterData;

namespace TNCSC.Hulling.Repository.Interfaces
{
    public interface IMasterDataRepository
    {
        public Task<APIResponse> AddOrUpdateGunnyCondition(GunnyCondition gunnyCondition);
        public Task<APIResponse> GetGunnyConditions();
        public Task<APIResponse> AddOrUpdateVarieryAndGrade(Variety variety);
        public Task<APIResponse> GetVarietyandGrades();
    }
}
