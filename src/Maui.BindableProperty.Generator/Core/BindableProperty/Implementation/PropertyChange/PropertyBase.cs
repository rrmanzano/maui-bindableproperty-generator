using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.PropertyChange;

public abstract class PropertyBase : IImplementation
{
    protected string PropertyName { get; set; }

    private TypedConstant OnEventAttr { get; set; }

    private IFieldSymbol FieldSymbol { get; set; }

    private INamedTypeSymbol ClassSymbol { get; set; }

    public PropertyBase(IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol, string attributeName)
    {
        var overridenPropertyName = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrPropertyName);

        OnEventAttr = fieldSymbol.GetTypedConstant(attributeSymbol, attributeName);
        PropertyName = AutoBindablePropertyGenerator.ChooseName(fieldSymbol.Name, overridenPropertyName);
        FieldSymbol = fieldSymbol;
        ClassSymbol = classSymbol;
    }

    public bool SetterImplemented()
    {
        return false;
    }

    public abstract string ProcessBindableParameters();

    public void ProcessBodySetter(CodeWriter w)
    {
        // Not implemented
    }

    protected abstract string GetInternalMethodName();

    protected abstract string GetMethodName();

    protected abstract string GetPartialMethodInvokation(string methodName, string formattedField);

    public void ProcessImplementationLogic(CodeWriter w)
    {
        var innerMethodName = this.GetInternalMethodName();
        var methodDefinition = @$"private static void {innerMethodName}({AutoBindableConstants.FullNameMauiControls}.BindableObject bindable, object oldValue, object newValue)";

        if (w.ToString().Contains(methodDefinition))
            return;

        var methodName = OnEventAttr.GetValue<string>() ?? GetMethodName();
        var formattedField = FieldSymbol.Type.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat);

        AttributeBuilder.WriteAllAttrGeneratedCodeStrings(w);
        using (w.B(methodDefinition))
        {
            w._($@"var ctrl = ({ClassSymbol.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)})bindable;");
            if (OnEventAttr.Value is string)
            {
                var methods = GetMethodsToCall(methodName);
                if (methods.Any())
                {
                    methods.ToList().ForEach(m =>
                    {
                        var count = m.Parameters.Count();
                        if (count == 0)
                            w._($@"ctrl.{methodName}();");
                        else if (count == 1)
                            w._($@"ctrl.{methodName}(({FieldSymbol.Type})newValue);");
                        else if (count == 2)
                            w._($@"ctrl.{methodName}(({FieldSymbol.Type})oldValue, ({FieldSymbol.Type})newValue);");
                    });
                }
            }
            else
            {
                w._(GetPartialMethodInvokation(methodName, formattedField));
            }
        }

        if (OnEventAttr.Value is not string)
        {
            w._($@"partial void {methodName}({formattedField} value);");
            w._();
        }
    }

    protected virtual IEnumerable<IMethodSymbol> GetMethodsToCall(string methodName)
    {
        var typeSymbol = FieldSymbol.Type;
        var methods = ClassSymbol.GetMembers(methodName)
                        .OfType<IMethodSymbol>()
                        .Where(m => m != null && (m.Parameters.Count() == 0 || m.Parameters.Count() <= 2 && m.Parameters.All(p => p.Type.Equals(typeSymbol, SymbolEqualityComparer.Default))));

        return methods.OrderBy(m => m.Parameters.Count());
    }
}
