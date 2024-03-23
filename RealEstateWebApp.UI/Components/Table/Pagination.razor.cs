using Microsoft.AspNetCore.Components;
using RealEstateWebApp.UI.Services;

namespace RealEstateWebApp.UI.Components.Table
{
    public partial class Pagination : ComponentBase
    {
        [Parameter] public int TotalSteps { get; set; } = 100;
        [Parameter] public int StepsSize { get; set; } = 3;
        [Parameter] public EventCallback<int> OnStepClick { get; set; }

        private List<int> DisplayedSteps { get; set; } = new List<int>();
        private int CurrentStep { get; set; } = 1;
        protected override void OnInitialized()
        {
            CalculateDisplayedSteps();
        }
        /*protected override void OnParametersSet()
        {
            CalculateDisplayedSteps();
        }*/
        /*protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
                CalculateDisplayedSteps();
        }*/

        private void CalculateDisplayedSteps()
        {
            int minStep = Math.Max(1, CurrentStep - StepsSize / 2);
            int maxStep = Math.Min(TotalSteps, minStep + StepsSize - 1);

            int offset = Math.Max(0, StepsSize - (maxStep - minStep + 1));
            minStep = Math.Max(1, minStep - offset);

            DisplayedSteps = new List<int>();
            for (int i = minStep; i <= maxStep; i++)
            {
                DisplayedSteps.Add(i);
            }
        }

        private async void OnStepClicked(int step)
        {
            if (step != CurrentStep)
            {
                CurrentStep = step;
                await OnStepClick.InvokeAsync(step);
            }
        }
    }
}
