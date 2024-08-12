using TNCSC.Hulling.Domain.Mill;
using TNCSC.Hulling.Domain;

namespace TNCSC.Hulling.Business.Interfaces
{
    public interface IMillService
    {
        public Task<APIResponse> AddNewMill(Mill millObj);
        public Task<APIResponse> GetMillDetailsByMillId(long millId);
        public Task<APIResponse> GetAllMills();
        public Task<APIResponse> UpdateMillDetails(Mill millObj);
    }
}
