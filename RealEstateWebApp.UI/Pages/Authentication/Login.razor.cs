using System.Globalization;
using System.Threading.Tasks;
using RealEstateWebApp.Business.Identity;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace RealEstateWebApp.UI.Pages.Authentication
{
    public partial class Login : ComponentBase
    {
        private string _login;
        private string _password;
        private string _returnUrl = "";
        private string _error = null;
        private SignInResult _attempt;

        [Inject] private UserService UserService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IIndicatorService Indicator { get; set; }
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        
        [Inject] private IWebHostEnvironment WebHostEnvironment { get; set; }

        protected override void OnInitialized()
        {
            if (WebHostEnvironment.IsDevelopment())
            {
                _login = "admin@admin.com";
                _password = "Password1!";
            }   
        }
        
        //todo for test
        // private Task Register()
        // {
        //     return UserManager.CreateAsync(new ApplicationUser()
        //     {
        //         Email = "admin@admin.com",
        //         UserName = "admin",
        //         EmailConfirmed = true,
        //         RoleId = 1,
        //     }, "Password1!");
        // }

        private async Task DoLogin()
        {
            if (string.IsNullOrWhiteSpace(_login) || string.IsNullOrWhiteSpace(_password))
            {
                //name can't be empty
                return;
            }

            await Indicator.Show("login");
            var hideIndicator = true;
            try
            {
                _attempt = await UserService.CheckPassword(_login, _password);
                if (_attempt.Succeeded)
                {
                    await JsRuntime.InvokeVoidAsync("Utils.submitForm", "#LoginForm");
                    hideIndicator = false;
                }
            }
            finally
            {
                if (hideIndicator)
                    await Indicator.HideForce("login");
            }
        }
    }
}