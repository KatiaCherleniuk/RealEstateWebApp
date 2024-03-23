using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.SelectComponent
{
    public partial class DropdownSelect<TValue>
    {
        [Parameter]
        public RenderFragment InitialTip { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private RenderFragment _text;

        private List<SelectItem<TValue>> _items;
        private TValue _value;

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool IsRemoveAllAllowed { get; set; } = false;

        [Parameter]
        public TValue Value
        {
            get => _value;
            set => SetNewValue(value);
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public EventCallback OnValueChanged { get; set; }

        protected override void OnInitialized()
        {
            _items = new List<SelectItem<TValue>>();
            _text = InitialTip;
        }

        private bool IsSame(TValue a, TValue b)
        {
            return EqualityComparer<TValue>.Default.Equals(a, b);
        }

        private void SetNewValue(TValue newValue)
        {
            if (IsSame(_value, newValue))
                return;
            _value = newValue;
            
            var item = _items?.FirstOrDefault(i => IsSame(i.Value, _value));
            if (item != null)
                _text = item.ChildContent(item.Value);
            else
                _text = InitialTip;
            
            if (ValueChanged.HasDelegate)
                ValueChanged.InvokeAsync(_value);
            if (OnValueChanged.HasDelegate)
                OnValueChanged.InvokeAsync(_value);
            
            InvokeAsync(StateHasChanged);
        }
        
        public void HandleSelect(SelectItem<TValue> selectItem)
        {
            Value = selectItem.Value;
        }

        public void AddItem(SelectItem<TValue> selectItem)
        {
            _items.Add(selectItem);
            var isSameValue = IsSame(_value, selectItem.Value);
            if (_value != null && isSameValue)
            {
                _text = selectItem.ChildContent(selectItem.Value);
                StateHasChanged();
            }
        }

        private void RemoveSelection()
        {
            Value = default(TValue);
        }
    }
}