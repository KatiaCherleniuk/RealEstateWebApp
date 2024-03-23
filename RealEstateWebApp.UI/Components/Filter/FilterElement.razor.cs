using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.UI.Services;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.Filter
{
    public partial class FilterElement : ComponentBase
    {
        [Parameter] public PropertyWithValuesModel Property { get; set; }
        
        [Inject] private FiltersWatcher FiltersWatcher { get; set; }

        private Timer _valueChangedTimer;
        private string _stringValue;

        private string _minValue;
        private string _maxValue;
        private int _selectItem;
        private List<int> _selectItems = new List<int>();
        

        private void ValueChanged()
        {
            Console.WriteLine($"FilterElement.TriggerEvent:{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
            var filter = MakeFilterModel();
            if (filter == null)
                FiltersWatcher.RemoveFilter(Property.Id);
            else
                FiltersWatcher.AddFilter(filter);
        }

        private BaseFilterValueModel MakeFilterModel()
        {
            switch (Property.Type)
            {
                case PropertyType.Text:
                    return string.IsNullOrEmpty(_stringValue) ? null : new StringFilterValueModel(Property.Id, _stringValue);
                
                case PropertyType.Int:
                case PropertyType.Float:
                    return MakeNumberFilterModel();
                
                case PropertyType.ListSingle:
                    return MakeValueIdFilterModel();
                
                case PropertyType.ListMultiple:
                    return MakeListValueIdFilterModel();
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ListFilterValueModel MakeListValueIdFilterModel()
        {
            if (_selectItems == null || _selectItems.Count == 0)
                return null;
            var res = new ListFilterValueModel(Property.Id)
            {
                Values = _selectItems
            };
            return res;
        }

        private ValueIdFilterValueModel MakeValueIdFilterModel()
        {
            if (_selectItem == 0)
                return null;
            var res = new ValueIdFilterValueModel(Property.Id)
            {
                ValueId = _selectItem
            };
            return res;
        }
        
        private NumberFilterValueModel MakeNumberFilterModel()
        {
            var resInt = new  NumberFilterValueModel(Property.Id);
            if (decimal.TryParse(_minValue, out var vInt))
                resInt.Min = vInt;
            if (decimal.TryParse(_maxValue, out vInt))
                resInt.Max = vInt;
            return resInt.Min == null && resInt.Max == null ? null : resInt;
        }
    }
}