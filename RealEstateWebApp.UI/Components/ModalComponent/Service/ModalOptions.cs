namespace RealEstateWebApp.UI.Components.ModalComponent.Service
{
    public class ModalOptions
    {
        public ModalPosition? Position { get; set; }
        public ModalSize? Size { get; set; }
        public string Class { get; set; }
        public bool? DisableBackgroundCancel { get; set; }
        public bool? DisableBackgroundColor { get; set; }
        public bool? HideHeader { get; set; }
        public bool? HideCloseButton { get; set; }
        public  bool?IsCenteredTitle { get; set; }
    }
}