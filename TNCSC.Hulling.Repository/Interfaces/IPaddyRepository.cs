#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Paddy;
#endregion

namespace TNCSC.Hulling.Repository.Interfaces
{
    /// <summary>
    /// IPaddyRepository
    /// </summary>
    public interface IPaddyRepository
    {
        public Task<APIResponse> AddPaddyDetails(Paddy paddyObj);
        public Task<APIResponse> GetAllDetails();
        public Task<APIResponse> GetPaddyDetailsByMillId(long millId);
        public Task<APIResponse> GetPaddyDetailsById(long id);
        public Task<APIResponse> UpdatePaddyDetails(Paddy paddyObj);
        public Task<APIResponse> PaddyMonthlyReport(long millId, string month);
    }
}
