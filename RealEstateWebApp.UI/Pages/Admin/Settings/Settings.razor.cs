using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.UI.Resources;

namespace RealEstateWebApp.UI.Pages.Admin.Settings
{
    public partial class Settings
    {
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public void GoToCategories(int CategoryId)
        {
            NavigationManager.NavigateTo($"/admin/settings/category/{CategoryId}");
        }
    }
}
