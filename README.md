# Maui.BindableProperty.Generator

Source generator that automatically transforms fields into BindableProperties that can be used in MAUI. 

## Installation
First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install [Maui.BindableProperty.Generator](https://www.nuget.org/packages/Maui.BindableProperty.Generator/) from the package manager console:
````bash
PM> Install-Package Maui.BindableProperty.Generator
````

## Usage - Simple implementation
Just decorate field with the Bindable attribute.

```csharp
    public partial class CustomButton : Button
    {
        [AutoBindable]
        private string _placeholder;
    }
```
the prevoius code will generate this:
```csharp
    public partial class CustomButton
    {
        public static readonly Microsoft.Maui.Controls.BindableProperty PlaceholderProperty = Microsoft.Maui.Controls.BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomButton), default(string));
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
    }
```

## Project status

- [x] Simple implementation - Done
- [x] Custom property name - In Progress
- [-] Custom Parameters - Pending
- [-] OnChanged method - Pending
- [-] Dependent properties - Pending

## Extra info
This repo is using part of the code used in this repo [CodeWriter](https://github.com/SaladLab/CodeWriter "CodeWriter") to generate the string used to create the CSharp files, thanks to the author.