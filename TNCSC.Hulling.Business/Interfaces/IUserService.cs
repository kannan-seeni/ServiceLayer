#region References
using TNCSC.Hulling.Domain;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// IUserService : Interface for UserService
    /// </summary>
    public interface IUserService
    {
        #region CreateUser
        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public Task<APIResponse> CreateUser(User userObj);
        #endregion

        #region GetUsersByMillId
        /// <summary>
        /// GetUsersByMillId
        /// </summary>
        /// <param name="millId"></param>
        /// <returns></returns>
        public Task<APIResponse> GetUsersByMillId(long millId);
        #endregion

        #region GetUserDetailsById
        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<APIResponse> GetUserDetailsById(long id);
        #endregion

        #region GetAllUser
        /// <summary>
        /// GetAllUser
        /// </summary>
        /// <returns></returns>
        public Task<APIResponse> GetAllUser();
        #endregion
    }
}
