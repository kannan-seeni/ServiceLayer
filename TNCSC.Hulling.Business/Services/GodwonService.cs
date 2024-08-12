#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Godwon;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// GodwonService
    /// </summary>
    public class GodwonService : IGodwonService
    {
        #region Declarations
        protected IGodwonRepository godwonRepository;
        #endregion

        #region Constructor
        public GodwonService(IGodwonRepository _godwonRepository)
        {
            godwonRepository = _godwonRepository;
        }
        #endregion

        #region AddNewGodwon
        public async Task<APIResponse> AddNewGodwon(Godwon godwonObj)
        {
            return await godwonRepository.AddNewGodwon(godwonObj);
        }
        #endregion

        #region GetAllGodwons
        public async Task<APIResponse> GetAllGodwons()
        {
            return await godwonRepository.GetAllGodwons();
        }
        #endregion

        #region GetGodwonDetailsByMillId
        public async Task<APIResponse> GetGodwonDetailsByMillId(long millId, int typeId)
        {
            return await godwonRepository.GetGodwonDetailsByMillId(millId, typeId);
        }
        #endregion

        #region GetGodwonDetailsById
        public async Task<APIResponse> GetGodwonDetailsById(long id)
        {
            return await godwonRepository.GetGodwonDetailsById(id);
        }
        #endregion

        #region UpdateGodwonDetails
        public async Task<APIResponse> UpdateGodwonDetails(Godwon godwonObj)
        {
            return await godwonRepository.UpdateGodwonDetails(godwonObj);
        }
        #endregion
    }
}


