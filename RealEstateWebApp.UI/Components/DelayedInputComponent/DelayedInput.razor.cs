using System;
using System.Threading;
using Microsoft.AspNetCore.Components;

namespace RealEstateWebApp.UI.Components.DelayedInputComponent
{
    public partial class DelayedInput : ComponentBase
    {
        [Parameter] public string ClassList { get; set; }
        [Parameter] public string Placeholder { get; set; }
        [Parameter] public int DelayMs { get; set; }

        [Parameter]
        public string Value
        {
            get => _value;
            set => _value = value;
        }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public EventCallback OnValueChanged { get; set; }
        
        private string _value;
        private Timer _valueChangedTimer;

        protected override void OnInitialized()
        {
            _valueChangedTimer = new Timer(TriggerEvent);
        }

        private void TriggerEvent(object state)
        {
            if (ValueChanged.HasDelegate)
                InvokeAsync(() => ValueChanged.InvokeAsync(_value));
            if (OnValueChanged.HasDelegate)
                InvokeAsync(() => OnValueChanged.InvokeAsync(_value));
        }

        private void OnInput(ChangeEventArgs arg)
        {
            var strVal = (string)arg.Value;
            if (string.Equals(_value, strVal, StringComparison.InvariantCultureIgnoreCase))
            {
                _value = strVal;
                return;
            }
            _value = strVal;
            _valueChangedTimer.Change(300, Timeout.Infinite);
        }
    }
}