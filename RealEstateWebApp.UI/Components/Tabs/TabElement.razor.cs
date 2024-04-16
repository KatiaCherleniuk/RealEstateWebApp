using System;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.Tabs
{
    public partial class TabElement : IDisposable
    {
        [CascadingParameter]
        public TabsContainerComponent Container { get; set; }
        
        [Parameter]
        public string Title { get; set; } 
        [Parameter]
        public string Class { get; set; }   
        
        [Parameter]
        public string Key { get; set; }
        
        [Parameter] 
        public RenderFragment ChildContent { get; set; }
        
        
        
        protected override void OnInitialized()
        {
            Container.AddTab(this);
        }

        public void Dispose()
        {
            Container.RemoveTab(this);
        }

        private void SelectTab()
        {
            Container.SetActivateTab(this);
        }
    }
}