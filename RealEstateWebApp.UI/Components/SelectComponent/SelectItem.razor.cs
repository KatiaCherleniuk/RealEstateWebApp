using System;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.SelectComponent
{
    public partial class SelectItem<TValue>
    {
        [CascadingParameter(Name = "SelectContainer")]
        public DropdownSelect<TValue> SelectContainer { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public RenderFragment<TValue> ChildContent { get; set; }
    
        protected override void OnInitialized()
        {
            // if you have null exception here check types (TValue) of SelectContainer and SelectItem
            SelectContainer.AddItem(this);
        }
    }
}