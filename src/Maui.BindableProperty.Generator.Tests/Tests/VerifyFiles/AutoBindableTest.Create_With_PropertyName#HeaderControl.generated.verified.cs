﻿//HintName: HeaderControl.generated.cs
// <auto-generated/>
#pragma warning disable
#nullable enable
namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl
    {
        /// <inheritdoc cref="_l"/>
        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        public static readonly Microsoft.Maui.Controls.BindableProperty LastNameProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(LastName),
                                                                typeof(string),
                                                                typeof(HeaderControl),
                                                                defaultValue: default(string?),
                                                                propertyChanged: __LastNameChanged,
                                                                propertyChanging: __LastNameChanging);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string? LastName
        {
            get => (string?)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __LastNameChanged(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnLastNameChanged((string?)newValue);
        }

        partial void OnLastNameChanged(string? value);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __LastNameChanging(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnLastNameChanging((string?)oldValue);
        }

        partial void OnLastNameChanging(string? value);

    }
}
