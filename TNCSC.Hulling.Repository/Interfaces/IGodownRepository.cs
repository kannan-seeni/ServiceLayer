#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Godown;
#endregion

namespace TNCSC.Hulling.Repository.Interfaces
{
    /// <summary>
    /// IGodownRepository
    /// </summary>
    public interface IGodownRepository
    {
        public Task<APIResponse> AddNewGodown(Godown godownObj);
        public Task<APIResponse> GetAllGodowns();
        public Task<APIResponse> GetGodownDetailsByRegion(int regionId, int typeId);
        public Task<APIResponse> GetGodownDetailsById(long id);
        public Task<APIResponse> UpdateGodownDetails(Godown godownObj);
        public Task<APIResponse> ActiveOrInActivateGodown(long godownId, bool status);
    }
}
