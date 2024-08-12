﻿#region References
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Domain.Rice;
#endregion

namespace TNCSC.Hulling.Repository.Interfaces
{
    /// <summary>
    /// IRiceRepository
    /// </summary>
    public interface IRiceRepository
    {
        public Task<APIResponse> AddRiceDetails(Rice riceObj);
        public Task<APIResponse> GetAllDetails();
        public Task<APIResponse> GetRiceDetailsByMillId(long millId);
        public Task<APIResponse> GetRiceDetailsById(long id);
        public Task<APIResponse> UpdateRiceDetails(Rice riceObj);
        public Task<APIResponse> RiceMonthReport(long millId, string month);
    }
}
