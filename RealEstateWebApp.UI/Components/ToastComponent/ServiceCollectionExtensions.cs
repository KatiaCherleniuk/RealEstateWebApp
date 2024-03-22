using RealEstateWebApp.UI.Components.ToastComponent.Services;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstateWebApp.UI.Components.ToastComponent
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorToast(this IServiceCollection services)
        {
            return services.AddScoped<IToastService, ToastService>();
        }
    }
}
