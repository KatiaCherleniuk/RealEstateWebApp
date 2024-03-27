using System;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.ModalComponent.Service
{
    public class ModalService : IModalService
    {
        internal Action<ModalReference> OnModalInstanceAdded;

        public ModalReference Show<T>() where T : ComponentBase
        {
            return Show<T>(string.Empty, new ModalParameters(), new ModalOptions());
        }

        public ModalReference Show<T>(string title) where T : ComponentBase
        {
            return Show<T>(title, new ModalParameters(), new ModalOptions());
        }

        public ModalReference Show<T>(string title, ModalOptions options) where T : ComponentBase
        {
            return Show<T>(title, new ModalParameters(), options);
        }
        public ModalReference Show<T>(string title, ModalParameters parameters) where T : ComponentBase
        {
            return Show<T>(title, parameters, new ModalOptions());
        }

        public ModalReference Show<T>(string title, ModalParameters parameters, ModalOptions options) where T : ComponentBase
        {
            return Show(typeof(T), title, parameters, options);
        }

        public ModalReference Show(Type contentComponent)
        {
            return Show(contentComponent, string.Empty, new ModalParameters(), new ModalOptions());
        }

        public ModalReference Show(Type contentComponent, string title)
        {
            return Show(contentComponent, title, new ModalParameters(), new ModalOptions());
        }

        public ModalReference Show(Type contentComponent, string title, ModalOptions options)
        {
            return Show(contentComponent, title, new ModalParameters(), options);
        }

        public ModalReference Show(Type contentComponent, string title, ModalParameters parameters)
        {
            return Show(contentComponent, title, parameters, new ModalOptions());
        }

        public ModalReference Show(Type contentComponent, string title, ModalParameters parameters, ModalOptions options)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(contentComponent))
            {
                throw new ArgumentException($"{contentComponent.FullName} must be a Blazor Component");
            }

            var modalInstanceId = Guid.NewGuid();
            var modalContent = new RenderFragment(builder => 
            {
                var i = 0;
                builder.OpenComponent(i++, contentComponent);
                foreach (var parameter in parameters._parameters)
                {
                    builder.AddAttribute(i++, parameter.Key, parameter.Value);
                }
                builder.CloseComponent(); 
            });
            var modalInstance = new RenderFragment(builder =>
            {
                builder.OpenComponent<BlazoredModalInstance>(0);
                builder.AddAttribute(1, "Options", options);
                builder.AddAttribute(2, "Title", title);
                builder.AddAttribute(3, "Content", modalContent);
                builder.AddAttribute(4, "Id", modalInstanceId);
                builder.CloseComponent();
            });
            var modalReference = new ModalReference(modalInstanceId, modalInstance);

            OnModalInstanceAdded?.Invoke(modalReference);

            return modalReference;
        }
    }
}