using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.UI.Components.ModalComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using RealEstateWebApp.UI.Pages.Authentication;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.UI.Shared.Navbar;
using RealEstateWebApp.UI.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace RealEstateWebApp.UI.Shared.Modals
{
    public partial class LeaveCommentModal: ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }
        [Parameter] public int RecordId { get; set; }
        [Inject] private IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private UserCommentsService UserCommentsService { get; set; }
        public CommentViewModel model { get; set; } = new();
        EditContext editContext;

        protected override void OnInitialized()
        {
            editContext = new(model);
        }
        private async Task Save()
        {
            var valid = editContext.Validate();
            if (!valid)
            {
                return;
            }
            var res = new UserCommentModel()
            {
                PhoneNumber = model.PhoneNumber,
                Comment = model.Comment,
                UserId = sessionUserResolver.User.Id,
                RecordId = RecordId,
                CreatedAt = DateTime.Now,
            };
            await UserCommentsService.SaveUserComment(res);
            BlazoredModal.Close(ModalResult.Ok());
        }
        private void Cancel()
        {
            BlazoredModal.Close(ModalResult.Cancel());
        }
    }
}
