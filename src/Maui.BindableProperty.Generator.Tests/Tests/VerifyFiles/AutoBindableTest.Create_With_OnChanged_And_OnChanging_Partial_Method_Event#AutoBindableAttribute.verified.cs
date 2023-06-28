//HintName: AutoBindableAttribute.cs

        #pragma warning disable
        #nullable enable
        using System;
        namespace Maui.BindableProperty.Generator.Core
        {
            public enum BindablePropertyAccessibility
            {
                /// <summary>
                /// If 'Undefined', bindable property will be defined in the same way as the class that contains it.
                /// </summary>
                Undefined = 0,

                /// <summary>
                /// Bindable property will be defined as 'private'
                /// </summary>
                Private = 1,

                /// <summary>
                /// Bindable property will be defined as 'private protected'
                /// </summary>
                ProtectedAndInternal = 2,

                /// <summary>
                /// Bindable property will be defined as 'protected'
                /// </summary>
                Protected = 3,

                /// <summary>
                /// Bindable property will be defined as 'internal'
                /// </summary>
                Internal = 4,

                /// <summary>
                /// Bindable property will be defined as 'protected internal'  
                /// </summary>     
                ProtectedOrInternal = 5,

                /// <summary>
                /// Bindable property will be defined as 'public'  
                /// </summary>
                Public = 6
            }

            [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
            [System.Diagnostics.Conditional("AutoBindableGenerator_DEBUG")]
            public sealed class AutoBindableAttribute : Attribute
            {
                public AutoBindableAttribute(){}

                public string PropertyName { get; set; } = string.Empty;

                public string? OnChanged { get; set; }

                public string? OnChanging { get; set; }

                public string? DefaultValue { get; set; }

                public string? DefaultBindingMode { get; set; }

                public string? ValidateValue { get; set; }

                public bool HidesUnderlyingProperty { get; set; } = false;

                public BindablePropertyAccessibility PropertyAccessibility { get; set; } = BindablePropertyAccessibility.Undefined;
            }
        }