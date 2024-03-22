using System;
using System.Threading.Tasks;

namespace RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent
{
    internal class LoaderContainer
    {
        public event Func<Task> HideEvent;
        public event Func<Task> ShowEvent;
        public int Counter;
        
        public LoaderContainer(Indicator indicator)
        {
            Subscribe(indicator);
            Counter = 0;
        }

        public LoaderContainer(int counter)
        {
            Counter = counter;
        }

        public void Subscribe(Indicator indicator)
        {
            ShowEvent += indicator.Show;
            HideEvent += indicator.Hide;
        }

        public void Unsubscribe(Indicator indicator)
        {
            ShowEvent -= indicator.Show;
            HideEvent -= indicator.Hide;
        }

        public virtual Task OnHideEvent()
        {
            return HideEvent?.Invoke() ?? Task.CompletedTask;
        }

        public virtual Task OnShowEvent()
        {
            return ShowEvent?.Invoke() ?? Task.CompletedTask;
        }
    }
}