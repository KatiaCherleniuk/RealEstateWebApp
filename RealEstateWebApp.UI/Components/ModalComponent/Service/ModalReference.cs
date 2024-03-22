using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.ModalComponent.Service
{
    public class ModalReference
    {
        private TaskCompletionSource<ModalResult> _resultCompletion = new TaskCompletionSource<ModalResult>();

        private Action<ModalResult> Closed;
        public Action<ModalResult> OnClosed;

        public ModalReference(Guid modalInstanceId, RenderFragment modalInstance)
        {
            Id = modalInstanceId;
            ModalInstance = modalInstance;
            Closed += HandleClosed;
        }

        private void HandleClosed(ModalResult obj)
        {
            _ = _resultCompletion.TrySetResult(obj);
            OnClosed?.Invoke(obj);
        }

        internal Guid Id { get; set; }
        internal RenderFragment ModalInstance { get; set; }

        public Task<ModalResult> Result => _resultCompletion.Task;

        internal void Dismiss(ModalResult result)
        {
            Closed.Invoke(result);
        }
    }
}