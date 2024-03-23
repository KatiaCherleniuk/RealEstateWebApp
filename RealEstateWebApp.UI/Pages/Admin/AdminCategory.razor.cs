using Microsoft.AspNetCore.Components;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models;
using RealEstateWebApp.UI.Services;

namespace RealEstateWebApp.UI.Pages.Admin
{
    public partial class AdminCategory : ComponentBase
    {
        [Parameter] public int CategoryId { get; set; } 
        [Parameter] public string Filter { get; set; }

        [Inject] private FiltersWatcher FiltersWatcher { get; set; }
        [Inject] private CategoryService CategoryService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private TitleAndIdModel _category;

        protected override async Task OnParametersSetAsync()
        {
            FiltersWatcher.SetCategoryId(CategoryId);
        }
        private void NavigateToDetails(int recordId)
        {
            NavigationManager.NavigateTo($"/admin/record/{recordId}/edit");
        }

    }
}
