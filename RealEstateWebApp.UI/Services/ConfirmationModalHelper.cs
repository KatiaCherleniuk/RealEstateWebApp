using System.Threading.Tasks;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Shared.Modals;

namespace RealEstateWebApp.UI.Services
{
    public static class ConfirmationModalHelper
    {
        public static async Task<bool> Confirm(this IModalService modalService, string title, string text=null, string yesBtnText=null, string noBtnText=null)
        {
            var parameters = new ModalParameters();
            parameters.Add("Text", text);
            parameters.Add("YesBtnText", yesBtnText);
            parameters.Add("NoBtnText", noBtnText);
            
            var res = modalService.Show<ConfirmModal>(
                title,
                parameters,
                new ModalOptions {Class = "confirmation-modal"});
            
            var modalResult = await res.Result;
            if (modalResult.Cancelled)
                return false;
            return (bool)modalResult.Data;
        }
    }
}