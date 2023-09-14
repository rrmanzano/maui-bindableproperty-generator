using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation;

public class DefaultValue : IImplementation
{
    private TypedConstant DefaultValueProperty { get; set; }
    private TypedConstant DefaultValueRawProperty { get; set; }
    private IFieldSymbol FieldSymbol { get; set; }

    public DefaultValue(IFieldSymbol fieldSymbol, ISymbol attributeSymbol)
    {
        this.DefaultValueProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrDefaultValue);
        this.DefaultValueRawProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrDefaultValueRaw);
        this.FieldSymbol = fieldSymbol;
    }

    public bool SetterImplemented()
    {
        return false;
    }

    public string ProcessBindableParameters()
    {
        var fieldType = this.FieldSymbol.Type;

        var hasRaw = false;
        var defaultValueRaw = this.DefaultValueRawProperty.GetValue<string>(value =>
        {
            hasRaw = true;
            return value;
        });
        if (hasRaw)
        {
            return defaultValueRaw != null ? $"defaultValue: {defaultValueRaw}" : $"defaultValue: default({fieldType.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)})";
        }

        var defaultValue = this.DefaultValueProperty.GetValue<string>(value =>
        {
            if (value != null)
            {
                if (fieldType.IsStringType())
                {
                    if (value == string.Empty)
                    {
                        return "string.Empty";
                    }

                    return $"\"{value}\"";
                }

                return value;
            }

            return default;
        });

        return defaultValue != null ? $"defaultValue: {defaultValue}" : $"defaultValue: default({fieldType.ToDisplayString(CommonSymbolDisplayFormat.DefaultFormat)})";
    }

    public void ProcessBodySetter(CodeWriter w)
    {
        // Not implemented
    }

    public void ProcessImplementationLogic(CodeWriter w)
    {
        // Not implemented
    }
}
