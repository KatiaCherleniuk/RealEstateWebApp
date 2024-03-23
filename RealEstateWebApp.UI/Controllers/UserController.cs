using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateWebApp.Business.Identity;

namespace RealEstateWebApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string login, [FromForm] string password, [FromQuery] string returnUrl)
        {
            var res = await _userService.Login(login, password, false, true);
            if (!res.Succeeded)
                return LocalRedirect("/login");

            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = "";

            var user = await _userService.GetUserByLogin(login);
            if(user.RoleId == 1)
                return Redirect("/" + returnUrl);

            return LocalRedirect("/" + returnUrl);
            //"/admin/dashboard"
        }

        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return LocalRedirect("/login");
        }
    }
}
