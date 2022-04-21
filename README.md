[![NuGet](http://img.shields.io/nuget/vpre/M.BindableProperty.Generator.svg?label=NuGet)](https://www.nuget.org/packages/M.BindableProperty.Generator/) [![GitHub issues](https://img.shields.io/github/issues/rrmanzano/maui-bindableproperty-generator?style=flat-square)](https://github.com/rrmanzano/maui-bindableproperty-generator/) [![GitHub stars](https://img.shields.io/github/stars/rrmanzano/maui-bindableproperty-generator?style=flat-square)](https://github.com/rrmanzano/maui-bindableproperty-generator/stargazers) ![last commit](https://img.shields.io/github/last-commit/rrmanzano/maui-bindableproperty-generator?style=flat-square)

# Maui.BindableProperty.Generator

Source generator that automatically transforms fields into BindableProperties that can be used in MAUI. 

## Installation
First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [M.BindableProperty.Generator](https://www.nuget.org/packages/M.BindableProperty.Generator/) from the package manager console:
````bash
PM> Install-Package M.BindableProperty.Generator
````

## Usage - Simple implementation
Just decorate field with the Bindable attribute.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class CustomEntry : ContentView
    {
        [AutoBindable]
        private string _placeholder;
    }
```
the prevoius code will generate this:
```csharp
    public partial class CustomEntry
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty PlaceholderProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntry), default(string));
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
    }
```

## Usage - Custom property name
Just decorate field with the Bindable attribute.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class CustomEntry : ContentView
    {
        [AutoBindable(PropertyName = "Text")]
        private string _t;
    }
```
the prevoius code will generate this:
```csharp
    public partial class CustomEntry
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty TextProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntry), default(string));
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
```

## Usage - OnChanged method 

### Example 1 - No Parameters
Just decorate field with the Bindable attribute.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private string _firstName;

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
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(FirstName), typeof(string), typeof(HeaderControl), defaultValue: default(string), propertyChanged: __UpdateDisplayName);

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
Just decorate field with the Bindable attribute. The 'UpdateDisplayName' method must have only one parameter (must match the type of the field)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private string _firstName;

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
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(FirstName), typeof(string), typeof(HeaderControl), defaultValue: default(string), propertyChanged: __UpdateDisplayName);

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
Just decorate field with the Bindable attribute. The 'UpdateDisplayName' method must have two parameters (must match the type of the field)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private string _firstName;

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
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(FirstName), typeof(string), typeof(HeaderControl), defaultValue: default(string), propertyChanged: __UpdateDisplayName);

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

## Usage - Set default value

### Example 1 - DateTime
Just decorate field with the Bindable attribute and add the "text/value" that you want to use as default value.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultValue = "DateTime.Now")]
        private DateTime _birthDate;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty BirthDateProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(BirthDate), typeof(System.DateTime), typeof(HeaderControl), defaultValue: DateTime.Now);

        public System.DateTime BirthDate
        {
            get => (System.DateTime)GetValue(BirthDateProperty);
            set => SetValue(BirthDateProperty, value);
        }
    }
```

### Example 2 - String
Just decorate field with the Bindable attribute and add the "text/value" that you want to use as default value.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultValue = "USA")]
        private string _country;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty CountryProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(Country), typeof(string), typeof(HeaderControl), defaultValue: "USA");

        public string Country
        {
            get => (string)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }
    }
```

## Usage - Set default BindingMode
Just decorate field with the Bindable attribute and add the "BindingMode" that you want to use as default value.

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultBindingMode = nameof(BindingMode.TwoWay))]
        private string _firstName;
    }
```
the prevoius code will generate this:
```csharp
    public partial class HeaderControl
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty FirstNameProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(FirstName), typeof(string), typeof(HeaderControl), defaultValue: default(string), defaultBindingMode: Microsoft.Maui.Controls.BindingMode.TwoWay);

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }
    }
```

## Project status

- ✅ Simple implementation - Done
- ✅ Custom property name - Done
- ✅ Custom Parameters - Done
- ✅ OnChanged method - Done
- ✅ OnChanged method overloading - Done

## Extra info
This repo is using part of the code of [CodeWriter](https://github.com/SaladLab/CodeWriter "CodeWriter") to generate the CSharp files, thanks to the author.