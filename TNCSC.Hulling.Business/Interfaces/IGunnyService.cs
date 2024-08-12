#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Gunnys;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// IGunnyService
    /// </summary>
    public interface IGunnyService
    {
        public Task<APIResponse> AddGunnyDetails(Gunny gunnyObj);
        public Task<APIResponse> GetAllDetails();
        public Task<APIResponse> GetGunnyDetailsByMillId(long millId);
        public Task<APIResponse> GetGunnyDetailsById(long id);
        public Task<APIResponse> UpdateGunnyDetails(Gunny gunnyObj);
    }
}
