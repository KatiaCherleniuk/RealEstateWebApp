using System;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent
{
    public static class Startup
    {
        public static IServiceCollection AddProgressIndicatorLite(this IServiceCollection services, Action<IndicatorOptions> optionsBuilder = null)
        {
            var options = new IndicatorOptions();
            optionsBuilder?.Invoke(options);
            services.AddScoped<IIndicatorService, IndicatorService>(_ => new IndicatorService
            {
                Options = options
            });
            return services;
        }
    }
}