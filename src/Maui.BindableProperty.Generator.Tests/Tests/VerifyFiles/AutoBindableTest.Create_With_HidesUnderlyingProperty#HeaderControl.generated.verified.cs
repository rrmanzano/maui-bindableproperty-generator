﻿//HintName: HeaderControl.generated.cs
// <auto-generated/>
#pragma warning disable
#nullable enable
namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl
    {
        /// <inheritdoc cref="_backgroundColor"/>
        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        public static new readonly Microsoft.Maui.Controls.BindableProperty BackgroundColorProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(BackgroundColor),
                                                                typeof(Color?),
                                                                typeof(HeaderControl),
                                                                defaultValue: Color.FromArgb("#cc3340"),
                                                                propertyChanged: __BackgroundColorChanged,
                                                                propertyChanging: __BackgroundColorChanging);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public new Color? BackgroundColor
        {
            get => (Color?)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __BackgroundColorChanged(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnBackgroundColorChanged((Color?)newValue);
        }

        partial void OnBackgroundColorChanged(Color? value);

        [global::System.CodeDom.Compiler.GeneratedCode("Maui.BindableProperty.Generator.Core.BindableProperty.AutoBindablePropertyGenerator", "0.11.1.0")]
        [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void __BackgroundColorChanging(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (global::Maui.BindableProperty.Generator.Demo.CustomControls.HeaderControl)bindable;
            ctrl.OnBackgroundColorChanging((Color?)oldValue);
        }

        partial void OnBackgroundColorChanging(Color? value);

    }
}
