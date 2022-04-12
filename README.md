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
Just decorate field with the Bindable attribute. The 'OnChanged' method must have only one parameter (must match the type of the field)

```csharp
    using Maui.BindableProperty.Generator.Core;

    public partial class CustomEntry : ContentView
    {
        [AutoBindable(OnChanged = nameof(OnTextChanged))]
        private string _text;

        private void OnTextChanged(string newValue)
        { 
            // Do stuff here
        }
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
            set =>
            {
                SetValue(TextProperty, value);
                this.OnTextChanged(value);
            }
        }
    }
```

## Project status

- ✅ Simple implementation - Done
- ✅ Custom property name - Done
- 🔲 Custom Parameters - In Progress
- ✅ OnChanged method - Done
- 🔲 Property Accessibility - Pending

## Extra info
This repo is using part of the code of [CodeWriter](https://github.com/SaladLab/CodeWriter "CodeWriter") to generate the CSharp files, thanks to the author.