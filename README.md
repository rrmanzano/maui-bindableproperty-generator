[![NuGet](http://img.shields.io/nuget/vpre/M.BindableProperty.Generator.svg?label=NuGet)](https://www.nuget.org/packages/M.BindableProperty.Generator/) [![GitHub issues](https://img.shields.io/github/issues/rrmanzano/maui-bindableproperty-generator?style=flat-square)](https://github.com/rrmanzano/maui-bindableproperty-generator/) [![GitHub stars](https://img.shields.io/github/stars/rrmanzano/maui-bindableproperty-generator?style=flat-square)](https://github.com/rrmanzano/maui-bindableproperty-generator/stargazers) ![last commit](https://img.shields.io/github/last-commit/rrmanzano/maui-bindableproperty-generator?style=flat-square)

# Maui.BindableProperty.Generator

Source generator that automatically transforms fields into BindableProperties that can be used in MAUI.  
Are you looking for the Xamarin project? check this [link](https://github.com/rrmanzano/xamarin-bindableproperty-generator)

## Installation
First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [M.BindableProperty.Generator](https://www.nuget.org/packages/M.BindableProperty.Generator/) from the package manager console:
````bash
PM> Install-Package M.BindableProperty.Generator
````

## Usage - Simple implementation
Just decorate the field with the Bindable attribute.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class CustomEntry : ContentView
    {
        [AutoBindable]
        private readonly string _placeholder;
    }
```
the prevoius code will generate this:
```csharp
    public partial class CustomEntry
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty PlaceholderProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(Placeholder),
                                                            typeof(string),
                                                            typeof(CustomEntry),
                                                            default(string));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
    }
```

## Options

| Option                    | Description                    | Type                             |
| -------------             | ------------------------------ | ------------------------------   |
| `PropertyName`            | Specifies the prefix used to create the BindableProperty, if `PropertyName` is null, then will take the name of the field removing the underscores and using PascalCase. Example: `_tex_t` will be transformed to `Text`. | `string`                         |
| `OnChanged`               | Specifies the name of the method to be executed when the property changed. | `string`                         |
| `DefaultValue`            | Specifies the "text/value" that will be used as default. Example: `default(string)`| `string`                         |
| `DefaultBindingMode`      | Specifies the "BindingMode" as string that will be used as default.  Options: `Default/TwoWay/OneWay/OneWayToSource/OneTime`.  Example: `nameof(BindingMode.TwoWay)`| `string`                         |
| `HidesUnderlyingProperty` | Specifies if the BindingProperty will hide the current implementation.       | `bool`                           |
| `ValidateValue`           | Specifies the name of the method to be executed to validate the values.       | `string`                         |

## Usage - **PropertyName**
Just decorate the field with the Bindable attribute.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class CustomEntry : ContentView
    {
        [AutoBindable(PropertyName = "Text")]
        private readonly string _t;
    }
```
the prevoius code will generate this:
```csharp
    public partial class CustomEntry
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty TextProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(Text),
                                                            typeof(string),
                                                            typeof(CustomEntry),
                                                            default(string));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
```

## Usage - **OnChanged**

### Example 1 - No Parameters
Just decorate the field with the Bindable attribute.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName;

        private void UpdateDisplayName()
        { 
            // Do stuff here
        }
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(FirstName),
                                                            typeof(string),
                                                            typeof(HeaderControl),
                                                            defaultValue: default(string),
                                                            propertyChanged: __UpdateDisplayName);

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        private static void __UpdateDisplayName(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (HeaderControl)bindable;
            ctrl.UpdateDisplayName();
        }
    }
```

### Example 2 - One Parameter
Just decorate the field with the Bindable attribute. The 'UpdateDisplayName' method must have only one parameter (must match the type of the field)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName;

        private void UpdateDisplayName(string newValue)
        { 
            // Do stuff here
        }
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(FirstName),
                                                            typeof(string),
                                                            typeof(HeaderControl),
                                                            defaultValue: default(string),
                                                            propertyChanged: __UpdateDisplayName);

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        private static void __UpdateDisplayName(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (HeaderControl)bindable;
            ctrl.UpdateDisplayName((string)newValue);
        }
    }
```

### Example 3 - Two Parameters
Just decorate the field with the Bindable attribute. The 'UpdateDisplayName' method must have two parameters (must match the type of the field)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName;

        private void UpdateDisplayName(string oldValue, string newValue)
        { 
            // Do stuff here
        }
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(FirstName),
                                                            typeof(string),
                                                            typeof(HeaderControl),
                                                            defaultValue: default(string),
                                                            propertyChanged: __UpdateDisplayName);

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        private static void __UpdateDisplayName(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)
        {
            var ctrl = (HeaderControl)bindable;
            ctrl.UpdateDisplayName((string)oldValue, (string)newValue);
        }
    }
```

## Usage - **DefaultValue**

### Example 1 - DateTime
Just decorate the field with the Bindable attribute and add the "text/value" that you want to use as default value.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultValue = "DateTime.Now")]
        private readonly DateTime _birthDate;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty BirthDateProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(BirthDate),
                                                            typeof(System.DateTime),
                                                            typeof(HeaderControl),
                                                            defaultValue: DateTime.Now);

        public System.DateTime BirthDate
        {
            get => (System.DateTime)GetValue(BirthDateProperty);
            set => SetValue(BirthDateProperty, value);
        }
    }
```

### Example 2 - String
Just decorate the field with the Bindable attribute and add the "text/value" that you want to use as default value.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultValue = "USA")]
        private readonly string _country;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty CountryProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(Country),
                                                            typeof(string),
                                                            typeof(HeaderControl),
                                                            defaultValue: "USA");

        public string Country
        {
            get => (string)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }
    }
```

## Usage - **DefaultBindingMode**
Just decorate the field with the Bindable attribute and add the "BindingMode" that you want to use as default value.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultBindingMode = nameof(BindingMode.TwoWay))]
        private readonly string _firstName;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty =
                                    Microsoft.Maui.Controls.BindableProperty.Create(
                                                            nameof(FirstName),
                                                            typeof(string),
                                                            typeof(HeaderControl),
                                                            defaultValue: default(string),
                                                            defaultBindingMode: Microsoft.Maui.Controls.BindingMode.TwoWay);

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }
    }
```

## Usage - **HidesUnderlyingProperty**
Just decorate the field with the Bindable attribute and set "HidesUnderlyingProperty = true".

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(HidesUnderlyingProperty = true)]
        private readonly Color _backgroundColor;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static new readonly Microsoft.Maui.Controls.BindableProperty BackgroundColorProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(BackgroundColor),
                                                                typeof(Microsoft.Maui.Graphics.Color),
                                                                typeof(HeaderControl));

        public new Microsoft.Maui.Graphics.Color BackgroundColor
        {
            get => (Microsoft.Maui.Graphics.Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
    }
```

## Usage - **ValidateValue**
### Example 1 - Non-Static Method
Just decorate the field with the Bindable attribute. The 'ValidateNotNull' method must have two parameters (one of type BindableObject and the second one must be the same as the field)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(ValidateValue = nameof(ValidateIsNullOrEmpty))]
        private readonly string? _country;

        private bool ValidateIsNullOrEmpty(BindableObject _, string? value)
        {
            // Do stuff here
            return !string.IsNullOrEmpty(value);
        }
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty CountryProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(Country),
                                                                typeof(string),
                                                                typeof(HeaderControl),
                                                                validateValue: __ValidateIsNullOrEmpty);

        public string? Country
        {
            get => (string?)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }

        private static bool __ValidateIsNullOrEmpty(Microsoft.Maui.Controls.BindableObject bindable, object value)
        {
            var ctrl = (HeaderControl)bindable;
            return ctrl.ValidateIsNullOrEmpty(ctrl, (string?)value);
        }
    }
```
### Example 2 - Static Method
Just decorate the field with the Bindable attribute. The 'ValidateNotNull' method must have two parameters (one of type BindableObject and the second one must be object type)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(ValidateValue = nameof(ValidateNotNull))]
        private readonly string? _country;

        private static bool ValidateNotNull(BindableObject _, object value) => value != null;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty CountryProperty =
                                        Microsoft.Maui.Controls.BindableProperty.Create(
                                                                nameof(Country),
                                                                typeof(string),
                                                                typeof(HeaderControl),
                                                                defaultValue: default(string?),
                                                                validateValue: ValidateNotNull);

        public string? Country
        {
            get => (string?)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }
    }
```

## Do you want to remove the compiler warning CS0169 ?
```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        #pragma warning disable CS0169

        [AutoBindable]
        private readonly string? _country;

        #pragma warning restore CS0169
    }
```

## Project status

- ✅ `PropertyName` - Done
- ✅ `OnChanged` - Done
- ✅ `DefaultValue` - Done
- ✅ `DefaultBindingMode` - Done
- ✅ `HidesUnderlyingProperty` - Done
- ✅ `ValidateValue` - Done

## Extra info
This repo is using part of the code of [CodeWriter](https://github.com/SaladLab/CodeWriter "CodeWriter") to generate the CSharp files, thanks to the author.