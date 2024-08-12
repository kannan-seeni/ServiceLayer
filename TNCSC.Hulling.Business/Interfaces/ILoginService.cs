#region References
using TNCSC.Hulling.Domain;
#endregion

namespace TNCSC.Hulling.Business.Interfaces
{
    /// <summary>
    /// ILoginService : Interface for LoginService
    /// </summary>
    public interface ILoginService
    {
        #region ValidateUser
        /// <summary>
        /// ValidateUser : To validate the userid and password to authorize the application
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<AuthResult> ValidateUser(string userId, string password);
        #endregion
    }
}
