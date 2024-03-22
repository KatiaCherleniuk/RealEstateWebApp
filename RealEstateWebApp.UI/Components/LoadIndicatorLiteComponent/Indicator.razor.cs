using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent
{
    public partial class Indicator : ComponentBase, IDisposable
    {
        [Parameter] public RenderFragment Loader { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string Name { get; set; } = null!;
        [Parameter] public ChildContentHideModes ChildContentChildContentHideMode { get; set; }
        [Parameter] public string CssClass { get; set; } = "";

        [Inject] private IIndicatorService IndicatorService { get; set; }


        private bool _isLoadingProcess = false;

        private RenderFragment RenderLoader()
        {
            if (Loader != null)
                return Loader;
            return builder =>
            {
                builder.OpenComponent(0, IndicatorService.Options.DefaultLoaderComponent);
                builder.CloseComponent();
            };
        }

        protected override void OnInitialized()
        {
            IndicatorService.Register(this);
        }

        public void Dispose()
        {
            IndicatorService.Unregister(this);
        }
        
        internal Task Hide()
        {
            _isLoadingProcess = false;
            return InvokeAsync(StateHasChanged);
        }
        
        internal Task Show()
        {
            _isLoadingProcess = true;
            return InvokeAsync(StateHasChanged);
        }

        private bool IsChildContentShouldBeInTree()
        {
            if (!_isLoadingProcess)
                return true;
            return ChildContentChildContentHideMode != ChildContentHideModes.RemoveFromTree;
        }
        
        private string ChildContentCss()
        {
            if (!_isLoadingProcess)
                return string.Empty;
            switch (ChildContentChildContentHideMode)
            {
                case ChildContentHideModes.CssDisplayNone:
                    return "display: none;";
                case ChildContentHideModes.CssVisibilityHidden:
                    return "visibility: hidden;";
                case ChildContentHideModes.RemoveFromTree:
                    return string.Empty;
                case ChildContentHideModes.DoNothing:
                    return string.Empty;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}