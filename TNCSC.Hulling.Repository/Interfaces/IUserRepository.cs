#region References
using TNCSC.Hulling.Domain;
#endregion

namespace TNCSC.Hulling.Repository.Interfaces
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    public interface IUserRepository
    {
        public Task<APIResponse> CreateUser(User userObj);
        public Task<APIResponse> GetUsersByMillId(long millId);
        public Task<APIResponse> GetUserDetailsById(long id);
        public Task<APIResponse> GetAllUser();
        public Task<APIResponse> ActiveOrInActivateUser(long userId, bool status);
    }
}
