#region References
using TNCSC.Hulling.Domain.Godwon;
using TNCSC.Hulling.Domain;
#endregion

namespace TNCSC.Hulling.Repository.Interfaces
{
    /// <summary>
    /// IGodwonRepository
    /// </summary>
    public interface IGodwonRepository
    {
        public Task<APIResponse> AddNewGodwon(Godwon godwonObj);
        public Task<APIResponse> GetAllGodwons();
        public Task<APIResponse> GetGodwonDetailsByMillId(long millId, int typeId);
        public Task<APIResponse> GetGodwonDetailsById(long id);
        public Task<APIResponse> UpdateGodwonDetails(Godwon godwonObj);
    }
}
