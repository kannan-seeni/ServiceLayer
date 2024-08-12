#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Paddy;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// PaddyService
    /// </summary>
    public class PaddyService : IPaddyService
    {
        protected IPaddyRepository paddyRepository;

        #region Constructor
        public PaddyService(IPaddyRepository _paddyRepository)
        {
            paddyRepository = _paddyRepository;
        }
        #endregion

        #region AddPaddyDetails
        public async Task<APIResponse> AddPaddyDetails(Paddy paddyObj)
        {
            return await paddyRepository.AddPaddyDetails(paddyObj);
        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            return await paddyRepository.GetAllDetails();
        }
        #endregion

        #region GetPaddyDetailsById
        public async Task<APIResponse> GetPaddyDetailsById(long id)
        {
            return await paddyRepository.GetPaddyDetailsById(id);
        }
        #endregion

        #region GetPaddyDetailsByMillId
        public async Task<APIResponse> GetPaddyDetailsByMillId(long millId)
        {
            return await paddyRepository.GetPaddyDetailsByMillId(millId);
        }
        #endregion

        #region UpdatePaddyDetails
        public async Task<APIResponse> UpdatePaddyDetails(Paddy paddyObj)
        {
            return await paddyRepository.UpdatePaddyDetails(paddyObj);
        }
        #endregion

        public async Task<APIResponse> PaddyMonthlyReport(long millId,string month)
        {
            return await paddyRepository.PaddyMonthlyReport(millId,month);
        }
    }
}

