using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation;

public class PropertyChanging : PropertyChanged
{
    public PropertyChanging(IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol): base(fieldSymbol, attributeSymbol, classSymbol)
    {
        
    }

    public override string ProcessBindableParameters()
    {
        return $@"propertyChanging: {GetInternalMethodName()}";
    }

    protected override string GetInternalMethodName()
    {
        return $@"__{this.PropertyName}Changing";
    }

    protected override string GetMethodName()
    {
        return $@"On{this.PropertyName}Changing";
    }

    protected override IEnumerable<IMethodSymbol> GetMethodsToCall(string methodName)
    {
        return Enumerable.Empty<IMethodSymbol>();
    }
}
