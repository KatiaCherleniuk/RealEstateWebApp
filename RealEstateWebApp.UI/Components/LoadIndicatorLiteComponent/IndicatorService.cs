using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent
{
    internal class IndicatorService : IIndicatorService
    {
        public IndicatorOptions Options { get; set; }
        private ConcurrentDictionary<string, LoaderContainer> _loaders;

        public async Task StartTask(Task action, string name)
        {
            try
            {
                await Show(name);
                await action;
            }
            finally
            {
                await Hide(name);
            }
        }

        public Task StartTask(Action action, string name)
        {
            return StartTask(Task.Run(action), name);
        }

        public Task StartTask(Func<Task> action, string name)
        {
            return StartTask(action(), name);
        }

        public Task Show(string name)
        {
            if (_loaders.TryGetValue(name, out var container))
            {
                Interlocked.Increment(ref container.Counter);
                return container.Counter == 1
                    ? container.OnShowEvent()
                    : Task.CompletedTask;
            }

            _loaders.TryAdd(name, new LoaderContainer(1));
            return Task.CompletedTask;
        }

        public Task Hide(string name)
        {
            if (_loaders.TryGetValue(name, out var container))
            {
                Interlocked.Decrement(ref container.Counter);
                if (container.Counter < 0)
                    container.Counter = 0;
                return container.Counter == 0
                    ? container.OnHideEvent()
                    : Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        public Task HideForce(string name)
        {
            if (_loaders.TryGetValue(name, out var container))
            {
                container.Counter = 0;
                return container.OnHideEvent();
            }

            return Task.CompletedTask;
        }

        void IIndicatorService.Register(Indicator indicator)
        {
            if (_loaders.TryGetValue(indicator.Name, out var container))
            {
                container.Subscribe(indicator);
                if (container.Counter > 0)
                    indicator.Show();
            }
            else
                _loaders.TryAdd(indicator.Name, new LoaderContainer(indicator));
        }

        void IIndicatorService.Unregister(Indicator indicator)
        {
            if (_loaders.TryGetValue(indicator.Name, out var container))
                container.Unsubscribe(indicator);
        }

        public IndicatorService()
        {
            _loaders = new ConcurrentDictionary<string, LoaderContainer>();
        }
    }
}