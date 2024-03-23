using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.Models.UserSettings;
using RealEstateWebApp.Models;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Services;

namespace RealEstateWebApp.UI.Pages.Admin
{
    public partial class RecordsAsTable : ComponentBase, IDisposable
    {
        [Parameter] public int CategoryId { get; set; }
        [Parameter] public EventCallback<int> OnRowClick { get; set; }
        [Inject] public ILogger<RecordsAsTable> Logger { get; set; }
        [Inject] public IIndicatorService IndicatorService { get; set; }
        [Inject] public FiltersWatcher FiltersWatcher { get; set; }
        [Inject] public RecordService RecordService { get; set; }
        [Inject] public CategoryService CategoryService { get; set; }
        [Inject] public PropertyService PropertyService { get; set; }

        private bool _areRecordsLoadingInProgress;
        private int _totalRecords;
        private List<RecordSimplifiedViewModel> _records;
        private List<PropertyWithValuesModel> _properties;
        private DotNetObjectReference<RecordsAsTable> _objRef;

        private TitleAndIdModel _category;

        private int _paginatioStepsSize;

        protected override async Task OnInitializedAsync()
        {
            FiltersWatcher.SetCategoryId(CategoryId);
            _properties = new List<PropertyWithValuesModel>();
            var a = await PropertyService.GetAllForCategory(CategoryId);
            _properties = a.ToList();
            _records = new List<RecordSimplifiedViewModel>();
            ClearRecords();
            FiltersWatcher.FiltersChanged += OnFiltersChanged;
            _objRef = DotNetObjectReference.Create(this);
        }

        private Task OnFiltersChanged()
        {
            ClearRecords();
            return LoadRecords();
        }

        private async Task LoadRecords()
        {
            if (_areRecordsLoadingInProgress)
                return;
            try
            {
                _areRecordsLoadingInProgress = true;
                await InvokeAsync(StateHasChanged);
                var responseModel = await RecordService.GetRecordsByFilterRequest(FiltersWatcher.FilterModel);
                _records = responseModel.Records.ToList();
                _totalRecords = responseModel.TotalItems;
                _paginatioStepsSize = _totalRecords / FiltersWatcher.FilterModel.PageSize;
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

        private void ClearRecords()
        {
            _areRecordsLoadingInProgress = false;
            _records.Clear();
            _totalRecords = 0;
        }

        protected override async Task OnParametersSetAsync()
        {
            CategoryService.GetOneTitleOnly(CategoryId).ContinueWith(t => _category = t.Result);
            ClearRecords();
            await LoadRecords();
        }

        private async Task HandleStepClick(int step)
        {
            if (_areRecordsLoadingInProgress)
                return;
            if (_records.Count >= _totalRecords)
                return;

            FiltersWatcher.FilterModel.Page = step;

            FiltersWatcher.ChangePageSilent(step);
            await LoadRecords();
        }

        public void Dispose()
        {
            _objRef?.Dispose();
            FiltersWatcher.FiltersChanged -= OnFiltersChanged;
        }

        private void DoOrderByProperty(PropertyWithValuesModel property)
        {
            // move order to filter watcher
            if (property.Type == PropertyType.ListMultiple)
                return;
            if (FiltersWatcher.FilterModel.Order is OrderByPropertyModel orderByProperty && orderByProperty.PropertyId == property.Id)
                orderByProperty.IsDesc = !orderByProperty.IsDesc;
            else
            {
                FiltersWatcher.FilterModel.Order = new OrderByPropertyModel()
                {
                    PropertyId = property.Id,
                    PropertyType = property.Type,
                    IsDesc = false
                };
            }

            FiltersWatcher.OrderWasChanged();
        }

        private void DoOrderByOwnField(string recordFieldName)
        {
            // move order to filter watcher
            if (FiltersWatcher.FilterModel.Order is OrderByRecordOwnFieldModel orderByRecordOwnFieldModel && orderByRecordOwnFieldModel.RecordFieldName == recordFieldName)
                orderByRecordOwnFieldModel.IsDesc = !orderByRecordOwnFieldModel.IsDesc;
            else
            {
                FiltersWatcher.FilterModel.Order = new OrderByRecordOwnFieldModel()
                {
                    RecordFieldName = recordFieldName,
                    IsDesc = false
                };
            }

            FiltersWatcher.OrderWasChanged();
        }

    }
}
