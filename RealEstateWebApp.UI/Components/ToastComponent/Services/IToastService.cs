﻿using System;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.ToastComponent.Services
{
    public interface IToastService
    {
        /// <summary>
        /// A event that will be invoked when showing a toast
        /// </summary>
        Action<ToastLevel, RenderFragment, string> OnShow { get; set; }

        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowInfo(string message, string heading = "");
        
        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowInfo(MarkupString messageHtml, string heading = "");

        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowInfo(RenderFragment message, string heading = "");

        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowSuccess(string message, string heading = "");
        
        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowSuccess(MarkupString messageHtml, string heading = "");

        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowSuccess(RenderFragment message, string heading = "");

        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowWarning(string message, string heading = "");
        
        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowWarning(MarkupString messageHtml, string heading = "");

        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowWarning(RenderFragment message, string heading = "");

        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowError(string message, string heading = "");
        
        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowError(MarkupString messageHtml, string heading = "");

        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowError(RenderFragment message, string heading = "");

        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast level to display</param>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowToast(ToastLevel level, string message, string heading = "");
        
        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast level to display</param>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowToast(ToastLevel level, MarkupString messageHtml, string heading = "");

        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast level to display</param>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        void ShowToast(ToastLevel level, RenderFragment message, string heading = "");
    }
}
