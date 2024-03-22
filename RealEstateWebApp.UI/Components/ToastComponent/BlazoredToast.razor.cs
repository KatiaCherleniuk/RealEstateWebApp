using System;
using RealEstateWebApp.UI.Components.ToastComponent.Configuration;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.ToastComponent
{
    public partial class BlazoredToast: ComponentBase
    {
        [CascadingParameter] private BlazoredToasts ToastsContainer { get; set; }

        [Parameter] public Guid ToastId { get; set; }
        [Parameter] public ToastSettings ToastSettings { get; set; }

        private void Close()
        {
            ToastsContainer.RemoveToast(ToastId);
        }
    }
}