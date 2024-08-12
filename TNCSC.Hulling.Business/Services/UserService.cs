#region References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// UserService
    /// </summary>
    public class UserService : IUserService
    {
        #region Declarations
        /// <summary>
        /// userRepository
        /// </summary>
        protected IUserRepository userRepository;
        #endregion

        #region UserService Constructor
        /// <summary>
        /// UserService Constructor
        /// </summary>
        /// <param name="_userRepository"></param>
        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        #endregion

        #region CreateUser
        /// <summary>
        /// CreateUser
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public async Task<APIResponse> CreateUser(User userObj)
        {
            return await userRepository.CreateUser(userObj);
        }
        #endregion

        #region GetAllUser
        /// <summary>
        /// GetAllUser
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllUser()
        {
            return await userRepository.GetAllUser();
        }
        #endregion

        #region GetUserDetailsById
        /// <summary>
        /// GetUserDetailsById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetUserDetailsById(long id)
        {
            return await userRepository.GetUserDetailsById(id);
        }
        #endregion

        #region GetUsersByMillId
        /// <summary>
        /// GetUsersByMillId
        /// </summary>
        /// <param name="millId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetUsersByMillId(long millId)
        {
            return await userRepository.GetUsersByMillId(millId);
        }
        #endregion
    }
}
