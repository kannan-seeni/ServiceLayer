#region  References
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Repository.Interfaces;
#endregion

namespace TNCSC.Hulling.Business.Services
{
    /// <summary>
    /// LoginService 
    /// </summary>
    public class LoginService : ILoginService
    {
        #region Declarations
        protected ILoginRepository loginRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// LoginService Constructor
        /// </summary>
        /// <param name="_loginRepository"></param>
        public LoginService(ILoginRepository _loginRepository)
        {
            loginRepository = _loginRepository;
        }
        #endregion

        #region ValidateUser
        /// <summary>
        /// To validate the user to authorize the application
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<AuthResult> ValidateUser(string userId, string password)
        {
            var user = await loginRepository.ValidateUser(userId, password);
            if (user == null)
            {
                var errorsLst = new List<string>();
                errorsLst.Add("user name or password is invalid");
                return new AuthResult
                {
                    status = false,
                    user = null,
                    errors = errorsLst
                };
            }

            return new AuthResult
            {
                status = true,
                user = user,
                errors = null
            };
        }
        #endregion
    }
}
