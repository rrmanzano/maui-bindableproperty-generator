﻿//HintName: HeaderControl.generated.cs
// <auto-generated/>
#pragma warning disable
#nullable enable
namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl
    {
        /// <inheritdoc cref="_placeholder"/>
        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        public static readonly Microsoft.Maui.Controls.BindableProperty PlaceholderProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(Placeholder),
                                                                typeof(string),
                                                                typeof(HeaderControl),
                                                                defaultValue: default(string),
                                                                propertyChanged: __PlaceholderChanged,
                                                                propertyChanging: __PlaceholderChanging);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __PlaceholderChanged(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnPlaceholderChanged((string)newValue);
        }

        partial void OnPlaceholderChanged(string value);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.0.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __PlaceholderChanging(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnPlaceholderChanging((string)oldValue);
        }

        partial void OnPlaceholderChanging(string value);

    }
}
