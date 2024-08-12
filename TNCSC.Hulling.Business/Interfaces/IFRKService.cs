#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.FRK;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// IFRKService
    /// </summary>
    public interface IFRKService
    {
        public Task<APIResponse> AddFRKDetails(FRK frkObj);
        public Task<APIResponse> GetAllDetails();
        public Task<APIResponse> GetFRKDetailsByMillId(long millId);
        public Task<APIResponse> GetFRKDetailsById(long id);
        public Task<APIResponse> UpdateFRKDetails(FRK frkObj);
    }
}
