using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Stats;
using RealEstateWebApp.UI.Shared.Navbar;

namespace RealEstateWebApp.UI.Pages.Admin
{
    public partial class AdminDashboard
    {
        [Inject] StatisticService StatisticService { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        public List<StatsRecordWithStatus> recordsByStatus { get; set; } =  new List<StatsRecordWithStatus>();
        public List<StatsUsersRegisteredCountByTime> users { get; set; } =  new List<StatsUsersRegisteredCountByTime>();
        public List<StatsRecordByCategories> categories { get; set; } =  new List<StatsRecordByCategories>();
        public List<StatsAveragePrices> prices { get; set; } =  new List<StatsAveragePrices>();
        protected override async Task OnInitializedAsync()
        {
            recordsByStatus = await StatisticService.GetRecordsByStatuses(DateTime.Today.AddDays(-30));
            users = await StatisticService.GetStatisticByUsers(IntervalType.Month, 12);
            categories = await StatisticService.GetRecordsByCategories(DateTime.Today.AddDays(-30));
            prices = await StatisticService.GetAveragePrices(IntervalType.Month, 3);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (prices.Any())
            {
                await JsRuntime.InvokeVoidAsync("Chart.func3", categories);

                await JsRuntime.InvokeVoidAsync("Chart.func1", prices);
                await JsRuntime.InvokeVoidAsync("Chart.func2", users, "Зареєстровано");
            }
            
            
        }
    }
}
