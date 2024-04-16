using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Category;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Components.Table;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.UI.Services;

namespace RealEstateWebApp.UI.Components.Settings
{
    public partial class CategoriesSettingsComponent : ComponentBase
    {
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] public CategoryService CategoryService { get; set; }
        [Inject] public FiltersWatcher FiltersWatcher { get; set; }
        [Inject] public IIndicatorService IndicatorService { get; set; }
        public IEnumerable<CategoryWithRecordsCountModel> Categories { get; set; } 
        [Parameter] public EventCallback<int> OnRowClick { get; set; }
        public PaginationModel PaginationModel { get; set; }

        private string _filter { get; set; }
        private int _paginationStepsSize { get; set; }
        private int _pageSize { get; set; } = 8;
        private int _currentStep { get; set; } = 1;
        private int _totalSize { get; set; } 
        protected override async Task OnInitializedAsync()
        {
            PaginationModel = new PaginationModel()
            {
                PageSize = 8,
                CurrentStep = 1
            };
            await ReloadData();
        }
        public async Task ReloadData()
        {
            await IndicatorService.StartTask(async () =>
            {
                _filter = _filter ?? string.Empty;
                var result = await CategoryService.GetAllWithFilter(_filter, _pageSize, _currentStep);
                Categories = result.Item1;
                PaginationModel.TotalListSize = result.Item2;

            }, "categories-settings");
           
        }
        public async Task HandleStepClick(int step)
        {
            await ReloadData();
        }
    }
}
