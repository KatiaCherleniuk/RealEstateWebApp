using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.Helpers
{
    public partial class TwoColumnsPage : ComponentBase
    {
        [Parameter] public RenderFragment Title { get; set; }
        
        [Parameter] public RenderFragment LeftSubTitle { get; set; }
        [Parameter] public RenderFragment LeftButtons { get; set; }
        [Parameter] public RenderFragment LeftBody { get; set; }
        
        [Parameter] public RenderFragment RightSubTitle { get; set; }
        [Parameter] public RenderFragment RightButtons { get; set; }
        [Parameter] public RenderFragment RightBody { get; set; }
    }
}