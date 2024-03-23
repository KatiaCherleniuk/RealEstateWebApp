using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.TagBoxComponent
{
    public partial class TagBox<TValue>
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public RenderFragment InitialTip { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool IsRemoveAllAllowed { get; set; } = false;
        [Parameter] public bool IsAddAllAllowed { get; set; } = false;

        [Parameter]
        public List<TValue> Value
        {
            get => _value;
            set => SetNewValue(value);
        }

        [Parameter] public EventCallback<List<TValue>> ValueChanged { get; set; }

        [Parameter] public EventCallback OnValueChanged { get; set; }

        private List<TValue> _value = new List<TValue>();
        private List<TagBoxItem<TValue>> _items;
        private RenderFragment _text;

        protected override void OnInitialized()
        {
            _items = new List<TagBoxItem<TValue>>();
            _text = InitialTip;
        }

        public void HandleSelect(TagBoxItem<TValue> item)
        {
            if (_value.Any(tv => IsSame(tv, item.Value)))
            {
                Remove(item.Value);
                return;
            }

            _value.Add(item.Value);
            item.Selected = true;

            InvokeChanges();
        }

        public void AddItem(TagBoxItem<TValue> item)
        {
            _items.Add(item);
            item.Selected = _value.Any(tv => IsSame(tv, item.Value));
        }

        private void SetNewValue(List<TValue> initialList)
        {
            if (IsListEqual(initialList, _value))
                return;
            if (!initialList.Any())
                return;
            foreach (var initialListItem in initialList)
            {
                var itemCheck = _items?.FirstOrDefault(i => IsSame(i.Value, initialListItem));
                if (itemCheck != null)
                    _value.Add(itemCheck.Value);
            }
            InvokeChanges();
        }

        private void InvokeChanges()
        {
            if (ValueChanged.HasDelegate)
                ValueChanged.InvokeAsync(_value);
            if (OnValueChanged.HasDelegate)
                OnValueChanged.InvokeAsync();

            InvokeAsync(StateHasChanged);
        }

        private void RemoveAll()
        {
            _value.Clear();
            foreach (var item in _items)
                item.Selected = false;
            InvokeChanges();
        }

        private void Remove(TValue value)
        {
            _value.Remove(value);
            var item = GetItemByValue(value);
            if (item != null)
                item.Selected = false;
            InvokeChanges();
        }

        private void AddAll()
        {
            foreach (var tagItem in _items)
            {
                if (_value.Any(tv => IsSame(tv, tagItem.Value)))
                    continue;
                _value.Add(tagItem.Value);
                tagItem.Selected = true;
            }

            InvokeChanges();
        }

        private bool IsSame(TValue a, TValue b)
        {
            return EqualityComparer<TValue>.Default.Equals(a, b);
        }

        private RenderFragment GetName(TValue value)
        {
            var item = GetItemByValue(value);
            if (item != null)
                return item.ChildContent(value);

            return null;
        }

        private TagBoxItem<TValue> GetItemByValue(TValue value)
        {
            return _items?.FirstOrDefault(i => IsSame(i.Value, value));
        }

        private string GetColor(TValue value)
        {
            var item = GetItemByValue(value);
            if (item != null)
                return item.HexColor;

            return string.Empty;
        }

        private bool IsListEqual(List<TValue> list1, List<TValue> list2)
        {
            if (list1 == null || list2 == null)
                return true;
            var firstNotSecond = list1.Except(list2).ToList();
            var secondNotFirst = list2.Except(list1).ToList();
            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}