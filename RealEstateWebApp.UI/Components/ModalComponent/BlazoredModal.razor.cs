using System;
using System.Collections.ObjectModel;
using System.Linq;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace RealEstateWebApp.UI.Components.ModalComponent
{
  public partial class BlazoredModal
    {
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter] public bool? HideHeader { get; set; }
        [Parameter] public bool? HideCloseButton { get; set; }
        [Parameter] public bool? DisableBackgroundCancel { get; set; }
        [Parameter] public bool? DisableBackgroundColor { get; set; }
        [Parameter] public ModalPosition? Position { get; set; }
        [Parameter] public bool IsCenteredTitle { get; set; }
        [Parameter] public string Class { get; set; }

        private readonly Collection<ModalReference> Modals = new Collection<ModalReference>();
        private readonly ModalOptions GlobalModalOptions = new ModalOptions();

        protected override void OnInitialized()
        {
            ((ModalService)ModalService).OnModalInstanceAdded += Update;
            NavigationManager.LocationChanged += CancelModals;

            GlobalModalOptions.Class = Class;
            GlobalModalOptions.DisableBackgroundCancel = DisableBackgroundCancel;
            GlobalModalOptions.DisableBackgroundColor = DisableBackgroundColor;
            GlobalModalOptions.HideCloseButton = HideCloseButton;
            GlobalModalOptions.HideHeader = HideHeader;
            GlobalModalOptions.Position = Position;
            GlobalModalOptions.IsCenteredTitle = IsCenteredTitle;
        }

        internal void CloseInstance(Guid Id)
        {
            DismissInstance(Id, ModalResult.Ok<object>(null));
        }

        internal void CancelInstance(Guid Id)
        {
            DismissInstance(Id, ModalResult.Cancel());
        }

        internal void DismissInstance(Guid Id, ModalResult result)
        {
            var reference = Modals.SingleOrDefault(x => x.Id == Id);

            if (reference == null) 
                return;
            reference.Dismiss(result);
            Modals.Remove(reference);
            StateHasChanged();
        }

        internal async void CancelAllModals()
        {
            foreach (var modalReference in Modals)
            {
                modalReference.Dismiss(ModalResult.Cancel());
            }

            Modals.Clear();
            await InvokeAsync(StateHasChanged);
        }

        private async void CancelModals(object sender, LocationChangedEventArgs e)
        {
            foreach (var modalReference in Modals)
            {
                modalReference.Dismiss(ModalResult.Cancel());
            }

            Modals.Clear();
            await InvokeAsync(StateHasChanged);
        }

        private async void Update(ModalReference modalReference)
        {
            Modals.Add(modalReference);
            await InvokeAsync(StateHasChanged);
        }
    }
}