using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models;
using RealEstateWebApp.UI.Services;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Shared.Navbar
{
    public partial class Items : ComponentBase //, IDisposable
    {
        /*[Inject] private CategoryListWatcher CategoryListWatcher { get; set; }
        [Inject] private CategoryService CategoryService { get; set; }*/
        
        private IEnumerable<TitleAndIdModel> _categories = new List<TitleAndIdModel>();
        protected override async Task OnInitializedAsync()
        {
            //CategoryListWatcher.ListChanged += ReloadCategories;
            await ReloadCategoriesAsync();
        }

        private void ReloadCategories()
        {
            ReloadCategoriesAsync();
        }
        
        private async Task ReloadCategoriesAsync()
        {
            // _categories = await CategoryService.GetAllTitleOnly();
            _categories = new List<TitleAndIdModel>();
        }

        /*public void Dispose()
        {
            CategoryListWatcher.ListChanged -= ReloadCategories;
        }*/
    }
}