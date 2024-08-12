#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Godwon;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// IGodwonService
    /// </summary>
    public interface IGodwonService
    {
        public Task<APIResponse> AddNewGodwon(Godwon godwonObj);
        public Task<APIResponse> GetAllGodwons();
        public Task<APIResponse> GetGodwonDetailsByMillId(long millId, int typeId);
        public Task<APIResponse> GetGodwonDetailsById(long id);
        public Task<APIResponse> UpdateGodwonDetails(Godwon godwonObj);
    }
}
