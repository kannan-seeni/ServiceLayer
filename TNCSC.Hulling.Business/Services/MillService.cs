using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Mill;
using TNCSC.Hulling.Repository.Interfaces;

namespace TNCSC.Hulling.Business.Services
{
    public class MillService :IMillService
    {
        protected IMillRepository millRepository;

        public MillService(IMillRepository millRepository)
        {
            this.millRepository = millRepository;
        }

        public async  Task<APIResponse> AddNewMill(Mill millObj)
        {
             return await millRepository.AddNewMill(millObj);
        }

        public async Task<APIResponse> GetAllMills()
        {
            return await millRepository.GetAllMills();
        }

        public async Task<APIResponse> GetMillDetailsByMillId(long millId)
        {
            return await millRepository.GetMillDetailsByMillId(millId);
        }

        public async Task<APIResponse> UpdateMillDetails(Mill millObj)
        {
            return await millRepository.UpdateMillDetails(millObj);
        }
    }
}
