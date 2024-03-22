using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace RealEstateWebApp.UI.Shared
{
    public partial class AuthenticationLayout : LayoutComponentBase
    {
        [CascadingParameter] protected Task<AuthenticationState> AuthStat { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var user = (await AuthStat).User;
            if(user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo($"/", true);
            }
        }
    }
}