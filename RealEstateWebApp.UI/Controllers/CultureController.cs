using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateWebApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture, culture)),
                        new CookieOptions
                        {
                            Expires = DateTimeOffset.UtcNow.AddYears(1),
                            IsEssential = true,
                            Path = "/",
                            HttpOnly = false,
                        }
                );
            }

            return LocalRedirect(redirectUri);
        }
    }
}
