using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace RealEstateWebApp.UI.Components.Tabs
{
    public partial class TabsContainerComponent
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }
        [Parameter] 
        public Action<TabElement> OnTabChanged { get; set; }
        
        private List<TabElement> _tabs;

        public TabElement CurrentTab;

        private ErrorBoundary? errorBoundary;     
        protected override void OnInitialized()
        {
            _tabs = new List<TabElement>();
        }
        public void AddTab(TabElement tabElement)
        {
            _tabs.Add(tabElement);
            CurrentTab ??= tabElement;
            StateHasChanged();
        }
        
        public void RemoveTab(TabElement tabElement)
        {
            _tabs.Remove(tabElement);
        }

        public void SetActivateTab(TabElement tab)
        {
            if (CurrentTab == tab)
                return;
            CurrentTab = tab;
            OnTabChanged?.Invoke(tab);
            errorBoundary?.Recover();
            StateHasChanged();
        }

        public void SetActivateTab(string key)
        {
            var tab = _tabs.FirstOrDefault(s => s.Key == key);
            if (tab != null) 
                SetActivateTab(tab);
        }

    }
}