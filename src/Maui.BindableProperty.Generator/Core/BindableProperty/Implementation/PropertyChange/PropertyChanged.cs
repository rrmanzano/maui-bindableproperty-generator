using Microsoft.CodeAnalysis;
using System.Reflection;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.PropertyChange;

public class PropertyChanged : PropertyBase
{
    public PropertyChanged(
        IFieldSymbol fieldSymbol,
        ISymbol attributeSymbol,
        INamedTypeSymbol classSymbol) : base(fieldSymbol, attributeSymbol, classSymbol, AutoBindableConstants.AttrOnChanged) {}

    public override string ProcessBindableParameters()
    {
        return $@"propertyChanged: {GetInternalMethodName()}";
    }

    protected override string GetInternalMethodName()
    {
        return $@"__{PropertyName}Changed";
    }

    protected override string GetMethodName()
    {
        return $@"On{PropertyName}Changed";
    }

    protected override string GetPartialMethodInvokation(string methodName, string formattedField)
    {
        return $@"ctrl.{methodName}(({formattedField})newValue);";
    }
}
