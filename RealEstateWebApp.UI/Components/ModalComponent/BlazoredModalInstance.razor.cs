using System;
using RealEstateWebApp.UI.ModalComponent.Service;
using Microsoft.AspNetCore.Components;
using RealEstateWebApp.UI.Components.ModalComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;

namespace RealEstateWebApp.UI.Components.ModalComponent
{
      public partial class BlazoredModalInstance
    {
        [CascadingParameter] private BlazoredModal Parent { get; set; }
        [CascadingParameter] private ModalOptions GlobalModalOptions { get; set; }

        [Parameter] public ModalOptions Options { get; set; }

        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment Content { get; set; }
        [Parameter] public Guid Id { get; set; }

        private string Position { get; set; }
        private string Size { get; set; }
        private string Class { get; set; }
        private bool HideHeader { get; set; }
        private bool HideCloseButton { get; set; }
        private bool DisableBackgroundCancel { get; set; }
        private bool DisableBackgroundColor { get; set; }
        private bool IsCenteredTitle { get; set; }

        protected override void OnInitialized()
        {
            ConfigureInstance();
        }

        public void SetTitle(string title)
        {
            Title = title;
            StateHasChanged();
        }

        public void Close()
        {
            Close(ModalResult.Ok<object>(null));
        }

        public void Close(ModalResult modalResult)
        {
            Parent.DismissInstance(Id, modalResult);
        }

        public void Cancel()
        {
            Parent.DismissInstance(Id, ModalResult.Cancel());
        }

        public void CancelAllOpenedModals()
        {
            Parent.CancelAllModals();
        }

        private void ConfigureInstance()
        {
            Position = SetPosition();
            Size = SetSize();
            Class = SetClass();
            HideHeader = SetHideHeader();
            HideCloseButton = SetHideCloseButton();
            DisableBackgroundCancel = SetDisableBackgroundCancel();
            DisableBackgroundColor = SetDisableBackgroundColor();
            IsCenteredTitle = SetIsTitlePositionCentered();
        }

        private string SetPosition()
        {
            ModalPosition position;
            if (Options.Position.HasValue)
            {
                position = Options.Position.Value;
            }
            else if (GlobalModalOptions.Position.HasValue)
            {
                position = GlobalModalOptions.Position.Value;
            }
            else
            {
                position = ModalPosition.Center;
            }

            return position switch
            {
                ModalPosition.Center => "blazored-modal-center",
                ModalPosition.TopLeft => "blazored-modal-topleft",
                ModalPosition.TopRight => "blazored-modal-topright",
                ModalPosition.BottomLeft => "blazored-modal-bottomleft",
                ModalPosition.BottomRight => "blazored-modal-bottomright",
                ModalPosition.BottomCenter => "blazored-modal-bottomcenter",
                _ => "blazored-modal-center"
            };
        }
        private string SetSize()
        {
            ModalSize size;
            if (Options.Size.HasValue)
            {
                size = Options.Size.Value;
            }
            else if (GlobalModalOptions.Position.HasValue)
            {
                size = GlobalModalOptions.Size.Value;
            }
            else
            {
                size = ModalSize.Medium;
            }

            return size switch
            {
                ModalSize.Small => "modal-sm",
                ModalSize.Large => "modal-lg",
                ModalSize.XLarge => "modal-xl",
                _ => ""
            };
        }

        private string SetClass()
        {
            if (!string.IsNullOrWhiteSpace(Options.Class))
                return Options.Class;

            if (!string.IsNullOrWhiteSpace(GlobalModalOptions.Class))
                return GlobalModalOptions.Class;

            return "blazored-modal";
        }

        private bool SetHideHeader()
        {
            if (Options.HideHeader.HasValue)
                return Options.HideHeader.Value;

            if (GlobalModalOptions.HideHeader.HasValue)
                return GlobalModalOptions.HideHeader.Value;

            return false;
        }

        private bool SetHideCloseButton()
        {
            if (Options.HideCloseButton.HasValue)
                return Options.HideCloseButton.Value;

            if (GlobalModalOptions.HideCloseButton.HasValue)
                return GlobalModalOptions.HideCloseButton.Value;

            return false;
        }

        private bool SetDisableBackgroundCancel()
        {
            if (Options.DisableBackgroundCancel.HasValue)
                return Options.DisableBackgroundCancel.Value;

            return GlobalModalOptions.DisableBackgroundCancel.HasValue && GlobalModalOptions.DisableBackgroundCancel.Value;
        }

        private bool SetDisableBackgroundColor()
        {
            if (Options.DisableBackgroundColor.HasValue)
                return Options.DisableBackgroundColor.Value;

            return GlobalModalOptions.DisableBackgroundColor.HasValue && GlobalModalOptions.DisableBackgroundColor.Value;
        }

        private bool SetIsTitlePositionCentered()
        {
            if (Options.IsCenteredTitle.HasValue)
                return Options.IsCenteredTitle.Value;

            return GlobalModalOptions.IsCenteredTitle.HasValue && GlobalModalOptions.IsCenteredTitle.Value;
        }

        private void HandleBackgroundClick()
        {
            if (DisableBackgroundCancel)
                return;

            Parent.CancelInstance(Id);
        }
    }
}