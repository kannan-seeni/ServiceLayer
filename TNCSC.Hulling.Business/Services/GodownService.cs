#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Godown;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// GodownService
    /// </summary>
    public class GodownService : IGodownService
    {
        #region Declarations
        protected IGodownRepository godownRepository;
        #endregion

        #region Constructor
        public GodownService(IGodownRepository _godownRepository)
        {
            godownRepository = _godownRepository;
        }
        #endregion

        #region AddNewGodown
        public async Task<APIResponse> AddNewGodown(Godown godownObj)
        {
            return await godownRepository.AddNewGodown(godownObj);
        }
        #endregion

        #region GetAllGodowns
        public async Task<APIResponse> GetAllGodowns()
        {
            return await godownRepository.GetAllGodowns();
        }
        #endregion

        #region GetGodownDetailsByRegion
        public async Task<APIResponse> GetGodownDetailsByRegion(int regionId, int typeId)
        {
            return await godownRepository.GetGodownDetailsByRegion(regionId, typeId);
        }
        #endregion

        #region GetGodownDetailsById
        public async Task<APIResponse> GetGodownDetailsById(long id)
        {
            return await godownRepository.GetGodownDetailsById(id);
        }
        #endregion

        #region UpdateGodownDetails
        public async Task<APIResponse> UpdateGodownDetails(Godown godwonObj)
        {
            return await godownRepository.UpdateGodownDetails(godwonObj);
        }
        #endregion

        #region ActiveOrInActivateGodown
        public async Task<APIResponse> ActiveOrInActivateGodown(long godownId, bool status)
        {
            return await godownRepository.ActiveOrInActivateGodown(godownId, status);
        }
        #endregion
    }
}


