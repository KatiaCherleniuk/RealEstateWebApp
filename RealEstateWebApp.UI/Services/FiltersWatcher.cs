using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.RecordViewModels;

namespace RealEstateWebApp.UI.Services
{
    public class FiltersWatcher
    {
        public FilterRequestModel FilterModel { get; private set; }

        public delegate Task FiltersChangedDelegate();
        public event FiltersChangedDelegate FiltersChanged;

        public FiltersWatcher()
        {
            FilterModel = new FilterRequestModel()
            {
                Filters = new List<BaseFilterValueModel>(),
                PageSize = 5,
                Page = 1,
                Order = new OrderByRecordOwnFieldModel() { IsDesc = true, RecordFieldName = "Id" }
            };
        }

        public void AddFilter(BaseFilterValueModel valueModel)
        {
            var index = FilterModel.Filters.FindIndex(f => f.PropertyId == valueModel.PropertyId);
            if (index < 0)
                FilterModel.Filters.Add(valueModel);
            else
                FilterModel.Filters[index] = valueModel;
            TriggerCallFilterChangedEvent();
        }

        public void ClearFilters()
        {
            TriggerCallFilterChangedEvent();
        }

        public void RemoveFilter(int propertyId)
        {
            FilterModel.Filters.RemoveAll(f => f.PropertyId == propertyId);
            TriggerCallFilterChangedEvent();
        }

        private void TriggerCallFilterChangedEvent()
        {
            FilterModel.PageSize = 5;
            FilterModel.Page = 1;
            FiltersChanged?.Invoke();
        }

        public void SetCategoryId(int categoryId)
        {
            FilterModel.CategoryId = categoryId;
            FilterModel.Filters.Clear();
            FilterModel.Order = new OrderByRecordOwnFieldModel()
            {
                RecordFieldName = "Id",
                IsDesc = true,
            };
            TriggerCallFilterChangedEvent();
        }

        public void ChangePageSilent(int newPage)
        {
            FilterModel.Page = newPage;
        }

        public void OrderWasChanged()
        {
            TriggerCallFilterChangedEvent();
        }
    }
}
