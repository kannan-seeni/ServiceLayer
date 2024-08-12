using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Mill;

namespace TNCSC.Hulling.Repository.Interfaces
{
    public interface IMillRepository
    {
        public Task<APIResponse> AddNewMill(Mill millObj);
        public Task<APIResponse> GetMillDetailsByMillId(long millId);
        public Task<APIResponse> GetAllMills();
        public Task<APIResponse> UpdateMillDetails(Mill millObj);
    }
}
