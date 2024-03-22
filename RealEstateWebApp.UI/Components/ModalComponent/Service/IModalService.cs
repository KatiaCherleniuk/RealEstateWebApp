using System;
using Microsoft.AspNetCore.Components;
using RealEstateWebApp.UI.Components.ModalComponent.Service;

namespace RealEstateWebApp.UI.ModalComponent.Service
{
    public interface IModalService
    {
        ModalReference Show<TComponent>() where TComponent : ComponentBase;
        ModalReference Show<TComponent>(string title) where TComponent : ComponentBase;
        ModalReference Show<TComponent>(string title, ModalOptions options) where TComponent : ComponentBase;
        ModalReference Show<TComponent>(string title, ModalParameters parameters) where TComponent : ComponentBase;
        ModalReference Show<TComponent>(string title, ModalParameters parameters, ModalOptions options) where TComponent : ComponentBase;
        ModalReference Show(Type component);
        ModalReference Show(Type component, string title);
        ModalReference Show(Type component, string title, ModalOptions options);
        ModalReference Show(Type component, string title, ModalParameters parameters);
        ModalReference Show(Type component, string title, ModalParameters parameters, ModalOptions options);
    }
}