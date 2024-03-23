using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.TagBoxComponent
{
    public partial class TagBoxItem<TValue>
    {
        [CascadingParameter(Name = "TagBox")] public TagBox<TValue> TagBox { get; set; }

        [Parameter] public TValue Value { get; set; }
        [Parameter] public string HexColor { get; set; }

        [Parameter] public RenderFragment<TValue> ChildContent { get; set; }
        public bool Selected { get; set; }

        protected override void OnInitialized()
        {
            TagBox.AddItem(this);
        }
    }
}