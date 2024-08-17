#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Godown;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// IGodwonService
    /// </summary>
    public interface IGodownService
    {
        public Task<APIResponse> AddNewGodown(Godown godownObj);
        public Task<APIResponse> GetAllGodowns();
        public Task<APIResponse> GetGodownDetailsByRegion(int regionId, int typeId);
        public Task<APIResponse> GetGodownDetailsById(long id);
        public Task<APIResponse> UpdateGodownDetails(Godown godownObj);
        public Task<APIResponse> ActiveOrInActivateGodown(long godownId, bool status);
    }
}
