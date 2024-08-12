#region References
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Contracts.V1.Requests;
using TNCSC.Hulling.Contracts.V1.Responses;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Repository.Helpers;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// AuthLoginController
    /// </summary>
    
    [SkipMyGlobalActionFilter]
    public class AuthLoginController : Controller
    {
        #region Declarations
        protected ILoginService loginService;
        private readonly JwtSettings jwtSettings;
        private string sVersion = "v1.0.0.1";
        #endregion

        #region Constructor
        public AuthLoginController(ILoginService _loginService, JwtSettings _jwtSettings)
        {
            loginService = _loginService;
            jwtSettings = _jwtSettings;
        }
        #endregion

        #region Login

        [HttpPost(ApiRoutes.Authenticate.login)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> Login([FromBody] AuthenticateLogin request)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.version = sVersion;

            var AuthResult = await loginService.ValidateUser(request.UserId, request.Password);
            if (!AuthResult.status)
            {
                var loginSuccessResponse1 = await TokenGeneratorForUserAsync(AuthResult);
                aPIResponse.data = loginSuccessResponse1;

                aPIResponse.responseCode = ResponseCode.LoginFailure;

                return Ok(aPIResponse);
            }
            else
            {
                // Pls add a log for the password mismatch 
            }

            var loginSuccessResponse = await TokenGeneratorForUserAsync(AuthResult);
            aPIResponse.data = loginSuccessResponse;
            aPIResponse.responseCode = ResponseCode.LoginSucess;
            return Ok(aPIResponse);

        }

        #endregion

        #region TokenGenerator
        private async Task<Object> TokenGeneratorForUserAsync(AuthResult _authResult)
        {
            if (_authResult.user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(new[] {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value:_authResult.user.ID.ToString()),
                    new Claim(type: JwtRegisteredClaimNames.Name, value: _authResult.user.FirstName + _authResult.user.LastName),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: "Id", value:_authResult.user.ID.ToString()),
                    new Claim(type: "UserId", value:_authResult.user.UserId.ToString())
                }),
                    Expires = DateTime.UtcNow.Add(jwtSettings.TokenLifeTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return (new LoginSuccessResponse
                {
                    Token = tokenHandler.WriteToken(token),
                    UserId = _authResult.user.UserId,
                    ID = _authResult.user.ID,
                    MillId = _authResult.user.MillRefId.ToString(),
                    UserName = _authResult.user.FirstName + " " + _authResult.user.LastName,
                    Role = _authResult.user.Role.ToString(),


                });

            }
            else
            {
                return (new LoginSuccessResponse
                {
                    Token = "",
                    RefreshToken = "",
                    UserStatus = "InValid UserName or Password / InActive User"

                });
            }


        }
        #endregion

    }
}
