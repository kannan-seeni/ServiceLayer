#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Rice;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// RiceService
    /// </summary>
    public class RiceService : IRiceService
    {
        #region Declarations
        protected IRiceRepository riceRepository;
        #endregion

        #region Constructor
        public RiceService(IRiceRepository _riceRepository)
        {
            riceRepository = _riceRepository;
        }
        #endregion

        #region AddRiceDetails
        public async Task<APIResponse> AddRiceDetails(Rice riceObj)
        {
            return await riceRepository.AddRiceDetails(riceObj);
        }
        #endregion

        #region GetAllDetails
        public async Task<APIResponse> GetAllDetails()
        {
            return await riceRepository.GetAllDetails();
        }
        #endregion

        #region GetRiceDetailsById
        public async Task<APIResponse> GetRiceDetailsById(long id)
        {
            return await riceRepository.GetRiceDetailsById(id);
        }
        #endregion

        #region GetRiceDetailsByMillId
        public async Task<APIResponse> GetRiceDetailsByMillId(long millId)
        {
            return await riceRepository.GetRiceDetailsByMillId(millId);
        }

        #endregion

        #region UpdateRiceDetails
        public async Task<APIResponse> UpdateRiceDetails(Rice riceObj)
        {
            return await riceRepository.UpdateRiceDetails(riceObj);
        }
        #endregion



        public async Task<APIResponse> RiceMonthReport(long millId, string month)
        {
            return await riceRepository.RiceMonthReport(millId,month);
        }
    }
}
