using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.Business;
using RealEstateWebApp.Business.Identity;
using RealEstateWebApp.Models.User;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Components.Table;
using RealEstateWebApp.UI.Resources;

namespace RealEstateWebApp.UI.Components.Settings
{
    public partial class UserSettingsComponent: ComponentBase
    {
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] public IIndicatorService IndicatorService { get; set; }
        [Inject] UserService UserService { get; set; }
        public IEnumerable<UserCreateModel> Users { get; set; }
        public PaginationModel PaginationModel { get; set; }
        [Parameter] public EventCallback<int> OnRowClick { get; set; }
        private string _filter { get; set; }
        protected override async Task OnInitializedAsync()
        {
            PaginationModel = new PaginationModel()
            {
                PageSize = 8,
                CurrentStep = 1
            };
            await ReloadData();
        }
        public async Task ReloadData()
        {
            await IndicatorService.StartTask(async () =>
            {
                _filter = _filter ?? string.Empty;
                var result = await UserService.GetAllGuests(_filter, PaginationModel.PageSize, PaginationModel.CurrentStep);
                Users = result.Item1;
                PaginationModel.TotalListSize = result.Item2;

            }, "users-settings");
        }
        public async Task HandleStepClick(int step)
        {
            await ReloadData();
        }
    }
}
