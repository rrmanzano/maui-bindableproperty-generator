﻿//HintName: HeaderControl.generated.cs
// <auto-generated/>
#pragma warning disable
#nullable enable
namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl
    {
        /// <inheritdoc cref="_firstName"/>
        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(FirstName),
                                                                typeof(string),
                                                                typeof(HeaderControl),
                                                                defaultValue: default(string),
                                                                propertyChanged: __FirstNameChanged,
                                                                propertyChanging: __FirstNameChanging,
                                                                defaultBindingMode: Microsoft.Maui.Controls.BindingMode.TwoWay);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __FirstNameChanged(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnFirstNameChanged((string)newValue);
        }

        partial void OnFirstNameChanged(string value);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __FirstNameChanging(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnFirstNameChanging((string)oldValue);
        }

        partial void OnFirstNameChanging(string value);

    }
}