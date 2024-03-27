using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using RealEstateWebApp.Business.Helpers;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Components.ModalComponent;
using RealEstateWebApp.UI.Resources;

namespace RealEstateWebApp.UI.Shared.Modals
{
    public partial class AddressModal : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }
        [Parameter] public int RecordId { get; set; }
        [Parameter] public AddressModel Address { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IStringLocalizer<Resource> Localizer { get; set; }

        private AddressModel _address { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _address = Address != null ? ObjectHelper.Copy(Address) : new AddressModel();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dotNetObjRef = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync("map.initMap", dotNetObjRef, _address.Latitude, _address.Longitude);
            }
        }
        [JSInvokable]
        public void OnAddressCoordinatesChanged(decimal lat, decimal lng, string addressName)
        {
            _address.Latitude = lat;
            _address.Longitude = lng;
            _address.DisplayName = addressName;
            StateHasChanged();
        }
        private async Task ToggleMap(ChangeEventArgs e)
        {
            bool disableMap = (bool)e.Value;
            _address = new AddressModel();
            _address.IsEnteredManually = disableMap;
            StateHasChanged();
            await JsRuntime.InvokeVoidAsync("map.toggleMap", disableMap);
        }

        private void Save()
        {
            _address.UpdatedAt = DateTime.Now;
            BlazoredModal.Close(ModalResult.Ok(_address));
        }
        private void Cancel()
        {
            BlazoredModal.Close(ModalResult.Cancel());
        }
    }
}
