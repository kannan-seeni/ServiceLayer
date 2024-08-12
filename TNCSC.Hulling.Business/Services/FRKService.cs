#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.FRK;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// FRKService
    /// </summary>
    public class FRKService : IFRKService
    {
        #region Declarations
        protected IFRKRepository frkRepository;
        #endregion

        #region Constructor
        public FRKService(IFRKRepository _frkRepository)
        {
            frkRepository = _frkRepository;
        }
        #endregion

        #region AddFRKDetails
        public async Task<APIResponse> AddFRKDetails(FRK frkObj)
        {
            return await frkRepository.AddFRKDetails(frkObj);
        }
        #endregion

        #region  GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            return await frkRepository.GetAllDetails();
        }
        #endregion

        #region GetFRKDetailsById
        public async Task<APIResponse> GetFRKDetailsById(long id)
        {
            return await frkRepository.GetFRKDetailsById(id);
        }
        #endregion

        #region GetFRKDetailsByMillId
        public async Task<APIResponse> GetFRKDetailsByMillId(long millId)
        {
            return await frkRepository.GetFRKDetailsByMillId(millId);
        }
        #endregion

        #region UpdateFRKDetails
        public async Task<APIResponse> UpdateFRKDetails(FRK frkObj)
        {
            return await frkRepository.UpdateFRKDetails(frkObj);
        }
        #endregion
    }
}
