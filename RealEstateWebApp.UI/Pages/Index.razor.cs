using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.Models;
using RealEstateWebApp.UI.Components.Table;
using RealEstateWebApp.UI.Components.Tabs;
using RealEstateWebApp.UI.Pages.User;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.UI.Services;

namespace RealEstateWebApp.UI.Pages
{
    public partial class Index
    {
        [Parameter] public string Filter { get; set; }
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] public RecordService RecordService { get; set; }
        [Inject] public CategoryService CategoryService { get; set; }
        [Inject] public FiltersWatcher FiltersWatcher { get; set; }
        [Inject] public ILogger<RecordsAsCards> Logger { get; set; }
        public TitleAndIdModel CurrentCategory { get; set; }
        private IEnumerable<TitleAndIdModel> _categories { get; set; }
        private FilterResponseModel _filteredRecords { get; set; } = new();
        private bool _areRecordsLoadingInProgress;
        private bool _notFound = true;
        private ServiceType? _categoryType;

        protected override async Task OnInitializedAsync()
        {
            _categories = await CategoryService.GetAllTitleOnly();
            CurrentCategory = _categories.First();
            _notFound = CurrentCategory == null;
            if (_notFound)
            {
                return;
            }
            FiltersWatcher.SetCategoryId(CurrentCategory.Id);
            FiltersWatcher.FiltersChanged += OnFiltersChanged;
            FiltersWatcher.FilterModel.PageSize = 8;

            await ReloadRecords();
        }
        private async Task ReloadRecords(int step = 1)
        {
            if (_areRecordsLoadingInProgress || _notFound)
                return;
            try
            {
                _areRecordsLoadingInProgress = true;
                await InvokeAsync(StateHasChanged);
                FiltersWatcher.FilterModel.Page = step;
                _filteredRecords = await RecordService.GetRecordsByFilterRequestForCards(FiltersWatcher.FilterModel, _categoryType);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
            }
            finally
            {
                _areRecordsLoadingInProgress = false;
                await InvokeAsync(StateHasChanged);
            }
        }
        private Task OnFiltersChanged()
        {
            return ReloadRecords(FiltersWatcher.FilterModel.Page);
        }
        public async void TabHasChanged(TabElement tab)
        {
            switch (tab.Key)
            {
                case "0":
                    _categoryType = null;
                    break;
                case "1":
                    _categoryType = ServiceType.Rent;
                    break;
                case "2":
                    _categoryType = ServiceType.Sale;
                    break;
            }
            await OnFiltersChanged();
        }
        public async void CategoryChanged()
        {
            FiltersWatcher.SetCategoryId(CurrentCategory.Id);
            await ReloadRecords(FiltersWatcher.FilterModel.Page);
        }
    }
}
