using System;

namespace RealEstateWebApp.UI.Components.ModalComponent.Service
{
    public class ModalResult
    {
        public object Data { get; }
        public Type DataType { get; }
        public bool Cancelled { get; }

        protected ModalResult(object data, Type resultType, bool cancelled)
        {
            Data = data;
            DataType = resultType;
            Cancelled = cancelled;
        }

        public static ModalResult Ok() => Ok(true, default);
        public static ModalResult Ok<T>(T result) => Ok(result, default);
        public static ModalResult Ok<T>(T result, Type modalType) => new ModalResult(result, typeof(T), false);

        public static ModalResult Cancel() => new ModalResult(default, typeof(object), true);

    }
}