using Microsoft.AspNetCore.Components;
using RealEstateWebApp.UI.Services;

namespace RealEstateWebApp.UI.Components.Table
{
    public partial class Pagination : ComponentBase
    {
        [Parameter] public PaginationModel PaginationModel { get; set; }
        [Parameter] public EventCallback<int> OnStepClick { get; set; }

        private List<int> DisplayedSteps { get; set; } = new List<int>();
        protected override void OnParametersSet()
        {
            CalculateDisplayedSteps();
        }

        private void CalculateDisplayedSteps()
        {
            int minStep = Math.Max(1, PaginationModel.CurrentStep - PaginationModel.StepsSize / 2);
            int maxStep = Math.Min(PaginationModel.TotalListSize, minStep + PaginationModel.StepsSize - 1);

            int offset = Math.Max(0, PaginationModel.StepsSize - (maxStep - minStep + 1));
            minStep = Math.Max(1, minStep - offset);

            DisplayedSteps = new List<int>();
            for (int i = minStep; i <= maxStep; i++)
            {
                DisplayedSteps.Add(i);
            }
        }

        private async void OnStepClicked(int step)
        {
            if (step != PaginationModel.CurrentStep)
            {
                PaginationModel.CurrentStep = step;
                await OnStepClick.InvokeAsync(step);
            }
        }
    }
}
