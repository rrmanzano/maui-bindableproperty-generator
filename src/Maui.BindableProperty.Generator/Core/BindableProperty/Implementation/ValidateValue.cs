using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation;

public class ValidateValue : IImplementation
{
    private TypedConstant ValidateValueProperty { get; set; }
    private IFieldSymbol FieldSymbol { get; set; }
    private INamedTypeSymbol ClassSymbol { get; set; }

    public ValidateValue(IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
    {
        this.ValidateValueProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrValidateValue);
        this.FieldSymbol = fieldSymbol;
        this.ClassSymbol = classSymbol;
    }

    public bool SetterImplemented()
    {
        return false;
    }

    public string ProcessBindableParameters()
    {
        return this.ValidateValueProperty.GetValue<string>(methodName => {
            var method = this.GetMethodsToCall(methodName).FirstOrDefault();
            if (!this.IsValidMethod(method))
                return default;

            if (method.IsStatic)
            {
                return $@"validateValue: {methodName}";
            }

            return $@"validateValue: __{methodName}";
        });
    }

    public void ProcessBodySetter(CodeWriter w)
    {
        // Not implemented
    }

    public void ProcessImplementationLogic(CodeWriter w)
    {
        this.ValidateValueProperty.GetValue<string>(methodName => {
            var method = this.GetMethodsToCall(methodName).FirstOrDefault();
            if (!this.IsValidMethod(method) || method.IsStatic)
            {
                return default;
            }

            var methodDefinition = @$"private static bool __{methodName}({AutoBindableConstants.FullNameMauiControls}.BindableObject bindable, object value)";
            if (w.ToString().Contains(methodDefinition))
                return default;

            AttributeBuilder.WriteAllAttrGeneratedCodeStrings(w);
            using (w.B(methodDefinition))
            {
                w._($@"var ctrl = ({this.ClassSymbol.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)})bindable;");
                w._($@"return ctrl.{methodName}(ctrl, ({this.FieldSymbol.Type})value);");
            }

            return default;
        });
    }


    private bool IsValidMethod(IMethodSymbol method)
    {
        return method is not null && method.Parameters.Count() == 2;
    }

    private IEnumerable<IMethodSymbol> GetMethodsToCall(string methodName)
    {
        var typeSymbol = this.FieldSymbol.Type;
        var methods = this.ClassSymbol.GetMembers(methodName)
                        .OfType<IMethodSymbol>()
                        .Where(m => m != null && m.Parameters.Count() == 2);

        return methods.OrderBy(m => m.Parameters.Count());
    }
}
