using System;
using System.Threading.Tasks;

namespace RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent
{
    public interface IIndicatorService
    {
        Task StartTask(Task action, string name);
        Task StartTask(Action action, string name);
        Task StartTask(Func<Task> action, string name);
        Task Show(string name);
        Task Hide(string name);
        Task HideForce(string name);

        IndicatorOptions Options { get; }

        internal void Register(Indicator indicator);
        internal void Unregister(Indicator indicator);
    }
}