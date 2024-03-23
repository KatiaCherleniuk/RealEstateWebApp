using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Property;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.Filter
{
    public partial class FilterBlock : ComponentBase
    {
        private enum FilterBlockState
        {
            Show,
            Hide,
            Static
        }
        
        [Parameter] public int CategoryId { get; set; }
        [Parameter] public string Filter { get; set; }

        [Inject] private PropertyService PropertyService { get; set; }
        
        private int _categoryId;
        private IEnumerable<PropertyWithValuesModel> _filters;
        private FilterBlockState _filterBlockState;

        protected override async Task OnParametersSetAsync()
        {
            if (_categoryId == CategoryId)
                return;
            _filterBlockState = FilterBlockState.Hide;
            _filters = new List<PropertyWithValuesModel>();
            _categoryId = CategoryId;
            await LoadFilters();
        }

        private async Task LoadFilters()
        {
            _filters = await PropertyService.GetFiltersForCategory(_categoryId);
        }

        private void ToggleShowModal()
        {
            _filterBlockState = _filterBlockState == FilterBlockState.Hide ? FilterBlockState.Show : FilterBlockState.Hide;
        }

        private void TogglePinModal()
        {
            _filterBlockState = _filterBlockState == FilterBlockState.Static ? FilterBlockState.Show : FilterBlockState.Static;
        }
    }
}