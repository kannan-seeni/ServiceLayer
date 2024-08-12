#region References
using TNCSC.Hulling.Domain.Paddy;
using TNCSC.Hulling.Domain;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// IPaddyService
    /// </summary>
    public interface IPaddyService
    {
        public Task<APIResponse> AddPaddyDetails(Paddy paddyObj);
        public Task<APIResponse> GetAllDetails();
        public Task<APIResponse> GetPaddyDetailsByMillId(long millId);
        public Task<APIResponse> GetPaddyDetailsById(long id);
        public Task<APIResponse> UpdatePaddyDetails(Paddy paddyObj);

        public Task<APIResponse> PaddyMonthlyReport(long millId, string month);
    }
}
