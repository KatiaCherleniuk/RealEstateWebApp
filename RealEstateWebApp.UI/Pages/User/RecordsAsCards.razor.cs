using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.UI.Components.Table;
using RealEstateWebApp.UI.Components.Tabs;
using RealEstateWebApp.UI.Pages.Admin;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.UI.Services;
using System.Security.Policy;

namespace RealEstateWebApp.UI.Pages.User
{
    public partial class RecordsAsCards
    {
        [Parameter] public string Filter { get; set; }

        [Parameter] public Func<int, Task> ReloadItems { get; set; }
        [Parameter] public Action<int> OnCardClick { get; set; }
        [Parameter] public FilterResponseModel FilteredData { get; set; }
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] public RecordService RecordService { get; set; }
        [Inject] public FileService FileService { get; set; }
        public PaginationModel PaginationModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            PaginationModel = new PaginationModel()
            {
                CurrentStep = 1,
                PageSize = 12
            };
        }
        protected override void OnParametersSet()
        {
            if(FilteredData != null)
                PaginationModel.TotalListSize = FilteredData.TotalItems;
        }
        private async Task HandleStepClick(int step)
        {
            if (FilteredData.Records.Count() >= PaginationModel.TotalListSize)
                return;
            PaginationModel.CurrentStep = step;

            ReloadItems?.Invoke(step);
        }
    }
}
