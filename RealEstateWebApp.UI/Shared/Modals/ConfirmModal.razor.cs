using System.Threading.Tasks;
using RealEstateWebApp.UI.Components.ModalComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Shared.Modals
{
    public partial class ConfirmModal
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        [Parameter] public string Text { get; set; }
        [Parameter] public string TextHtml { get; set; }
        [Parameter] public string YesBtnText { get; set; }
        [Parameter] public string NoBtnText { get; set; }

        private void No()
        {
            BlazoredModal.Close(ModalResult.Ok(false));
        }

        private void Yes()
        {
            BlazoredModal.Close(ModalResult.Ok(true));
        }

    }
}