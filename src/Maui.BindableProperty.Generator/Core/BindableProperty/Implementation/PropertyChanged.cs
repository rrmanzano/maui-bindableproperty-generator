using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation;

public class PropertyChanged : IImplementation
{
    private TypedConstant OnChangedProperty { get; set; }

    protected string PropertyName { get; set; }

    private IFieldSymbol FieldSymbol { get; set; }
    private INamedTypeSymbol ClassSymbol { get; set; }

    public PropertyChanged(IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
    {
        this.OnChangedProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrOnChanged);
        var nameProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrPropertyName);

        this.PropertyName = AutoBindablePropertyGenerator.ChooseName(fieldSymbol.Name, nameProperty);
        this.FieldSymbol = fieldSymbol;
        this.ClassSymbol = classSymbol;
    }

    public bool SetterImplemented()
    {
        return false;
    }

    public virtual string ProcessBindableParameters()
    {
        return $@"propertyChanged: {GetInternalMethodName()}";
    }

    public void ProcessBodySetter(CodeWriter w)
    {
        // Not implemented
    }

    protected virtual string GetInternalMethodName()
    {
        return $@"__{this.PropertyName}Changed";
    }

    protected virtual string GetMethodName()
    {
        return $@"On{this.PropertyName}Changed";
    }

    public void ProcessImplementationLogic(CodeWriter w)
    {
        var innerMethodName = GetInternalMethodName();

        var methodName = GetMethodName();
        var methodDefinition = @$"private static void {innerMethodName}({AutoBindableConstants.FullNameMauiControls}.BindableObject bindable, object oldValue, object newValue)";

        if (w.ToString().Contains(methodDefinition))
            return;

        AttributeBuilder.WriteAllAttrGeneratedCodeStrings(w);
        using (w.B(methodDefinition))
        {
            w._($@"var ctrl = ({this.ClassSymbol.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)})bindable;");
            if (this.OnChangedProperty.Value is string customMethodName)
            {
                var methods = this.GetMethodsToCall(customMethodName);
                if (methods.Any())
                {
                    methods.ToList().ForEach(m =>
                    {
                        var count = m.Parameters.Count();
                        if (count == 0)
                            w._($@"ctrl.{customMethodName}();");
                        else if (count == 1)
                            w._($@"ctrl.{customMethodName}(({this.FieldSymbol.Type})newValue);");
                        else if (count == 2)
                            w._($@"ctrl.{customMethodName}(({this.FieldSymbol.Type})oldValue, ({this.FieldSymbol.Type})newValue);");
                    });
                }
            }
            if (this.OnChangedProperty.Value is not string clashingCustomName || clashingCustomName != methodName)
            {
                w._($@"ctrl.{methodName}(({this.FieldSymbol.Type.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)})newValue);");
            }
        }
        if (this.OnChangedProperty.Value is not string clashingCustomCallName || clashingCustomCallName != methodName)
        {
            w._($@"partial void {methodName}({this.FieldSymbol.Type.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)} value);");
        }
    }

    protected virtual IEnumerable<IMethodSymbol> GetMethodsToCall(string methodName)
    {
        var typeSymbol = this.FieldSymbol.Type;
        var methods = this.ClassSymbol.GetMembers(methodName)
                        .OfType<IMethodSymbol>()
                        .Where(m => m != null && (m.Parameters.Count() == 0 || (m.Parameters.Count() <= 2 && m.Parameters.All(p => p.Type.Equals(typeSymbol, SymbolEqualityComparer.Default)))));

        return methods.OrderBy(m => m.Parameters.Count());
    }
}
