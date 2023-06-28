using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.PropertyChange;

public class PropertyChanging : PropertyBase
{
    public PropertyChanging(
        IFieldSymbol fieldSymbol,
        ISymbol attributeSymbol,
        INamedTypeSymbol classSymbol) : base(fieldSymbol, attributeSymbol, classSymbol, AutoBindableConstants.AttrOnChanging) {}

    public override string ProcessBindableParameters()
    {
        return $@"propertyChanging: {GetInternalMethodName()}";
    }

    protected override string GetInternalMethodName()
    {
        return $@"__{PropertyName}Changing";
    }

    protected override string GetMethodName()
    {
        return $@"On{PropertyName}Changing";
    }

    protected override string GetPartialMethodInvokation(string methodName, string formattedField)
    {
        return $@"ctrl.{methodName}(({formattedField})oldValue);";
    }
}
