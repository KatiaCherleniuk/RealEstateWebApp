using System;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.ToastComponent.Services
{
    public class ToastService : IToastService
    {
        /// <summary>
        /// A event that will be invoked when showing a toast
        /// </summary>
        public Action<ToastLevel, RenderFragment, string> OnShow { get; set; }

        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowInfo(string message, string heading = "")
        {
            ShowToast(ToastLevel.Info, message, heading);
        }
        
        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>

        public void ShowInfo(MarkupString messageHtml, string heading = "")
        {
            ShowToast(ToastLevel.Info, messageHtml, heading);
        }

        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowInfo(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Info, message, heading);
        }

        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowSuccess(string message, string heading = "")
        {
            ShowToast(ToastLevel.Success, message, heading);
        }
        
        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowSuccess(MarkupString messageHtml, string heading = "")
        {
            ShowToast(ToastLevel.Success, messageHtml, heading);
        }

        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowSuccess(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Success, message, heading);
        }

        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowWarning(string message, string heading = "")
        {
            ShowToast(ToastLevel.Warning, message, heading);
        }
        
        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowWarning(MarkupString messageHtml, string heading = "")
        {
            ShowToast(ToastLevel.Warning, messageHtml, heading);
        }

        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowWarning(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Warning, message, heading);
        }

        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowError(string message, string heading = "")
        {
            ShowToast(ToastLevel.Error, message, heading);
        }
        
        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowError(MarkupString messageHtml, string heading = "")
        {
            ShowToast(ToastLevel.Error, messageHtml, heading);
        }

        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowError(RenderFragment message, string heading = "")
        {
            ShowToast(ToastLevel.Error, message, heading);
        }

        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast level to display</param>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowToast(ToastLevel level, string message, string heading = "")
        {
            ShowToast(level, builder => builder.AddContent(0, message), heading);
        }

        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast level to display</param>
        /// <param name="messageHtml">Html Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowToast(ToastLevel level, MarkupString messageHtml, string heading = "")
        {
            ShowToast(level, builder => builder.AddContent(0, messageHtml), heading);
        }


        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast level to display</param>
        /// <param name="message">RenderFragment to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowToast(ToastLevel level, RenderFragment message, string heading = "")
        {
            OnShow?.Invoke(level, message, heading);
        }
    }
}
