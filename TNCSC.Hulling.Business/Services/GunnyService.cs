#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Gunnys;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// GunnyService
    /// </summary>
    public class GunnyService : IGunnyService
    {
        #region Declarations
        protected IGunnyRepository gunnyRepository;
        #endregion

        #region Constructor
        public GunnyService(IGunnyRepository _gunnyRepository)
        {
            gunnyRepository = _gunnyRepository;
        }
        #endregion

        #region AddGunnyDetails
        public async Task<APIResponse> AddGunnyDetails(Gunny gunnyObj)
        {
            return await gunnyRepository.AddGunnyDetails(gunnyObj);
        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            return await gunnyRepository.GetAllDetails();
        }
        #endregion

        #region GetGunnyDetailsById
        public async Task<APIResponse> GetGunnyDetailsById(long id)
        {
            return await gunnyRepository.GetGunnyDetailsById(id);
        }
        #endregion

        #region GetGunnyDetailsByMillId
        public async Task<APIResponse> GetGunnyDetailsByMillId(long millId)
        {
            return await gunnyRepository.GetGunnyDetailsByMillId(millId);
        }
        #endregion

        #region UpdateGunnyDetails
        public async Task<APIResponse> UpdateGunnyDetails(Gunny gunnyObj)
        {
            return await gunnyRepository.UpdateGunnyDetails(gunnyObj);
        }
        #endregion
    }
}
