using System;
using System.ComponentModel;

namespace RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent
{
    public enum ChildContentHideModes
    {
        CssDisplayNone = 0,
        CssVisibilityHidden = 1,
        RemoveFromTree = 2,
        DoNothing = 3
    }
    
    public class IndicatorOptions
    {
        public ChildContentHideModes ChildContentChildContentHideMode { get; set; } = ChildContentHideModes.CssDisplayNone;

        public IndicatorOptions SetLoaderComponent<T>() where T : Component
        {
            DefaultLoaderComponent = typeof(T);
            return this;
        }
        
        public Type DefaultLoaderComponent { get; private set; } = typeof(DefaultLoader);
    }

}
