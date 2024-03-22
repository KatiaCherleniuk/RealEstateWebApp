using Microsoft.Extensions.DependencyInjection;
using RealEstateWebApp.UI.ModalComponent.Service;

namespace RealEstateWebApp.UI.Components.ModalComponent.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazoredModal(this IServiceCollection services)
        {
            return services.AddScoped<IModalService, ModalService>();
        }
    }
}