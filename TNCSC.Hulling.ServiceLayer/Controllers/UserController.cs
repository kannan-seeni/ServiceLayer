#region References
using Microsoft.AspNetCore.Mvc;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Components.Filters;
using TNCSC.Hulling.Contracts.V1;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.ServiceLayer.Filters;
#endregion

namespace TNCSC.Hulling.ServiceLayer.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    [SkipMyGlobalActionFilter]
    public class UserController : Controller
    {
        #region Declarations
        protected IUserService userService;
        APIResponse aPIResponse = new APIResponse();
        #endregion

        #region 
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }
        #endregion

        #region CreateUser

        [HttpPost(ApiRoutes.User.createUser)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> CreateUser([FromBody] User userObj)
        {
            var response = await userService.CreateUser(userObj);

            return Ok(response);

        }

        #endregion

        #region GetAllUsers

        [HttpGet(ApiRoutes.User.getAllUsers)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await userService.GetAllUser();

            return Ok(response);

        }

        #endregion

        #region GetUserByMillId

        [HttpGet(ApiRoutes.User.getUserByMillId)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetUserByMillId([FromRoute] long millId)
        {
            var response = await userService.GetUsersByMillId(millId);

            return Ok(response);

        }

        #endregion

        #region GetUserDetails

        [HttpGet(ApiRoutes.User.getUserDetails)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> GetUserDetails([FromRoute] long id)
        {
            var response = await userService.GetUserDetailsById(id);

            return Ok(response);

        }

        #endregion

        #region ActiveOrInActivateUser

        [HttpPut(ApiRoutes.User.activeOrInactiveUser)]
        [ServiceFilter(typeof(AuditAttribute))]
        public async Task<IActionResult> ActiveOrInActivateUser([FromRoute] long id, [FromRoute] bool status)
        {
            var response = await userService.ActiveOrInActivateUser(id, status);

            return Ok(response);

        }

        #endregion


    }
}
