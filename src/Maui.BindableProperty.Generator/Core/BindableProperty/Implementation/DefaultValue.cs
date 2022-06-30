using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation
{
    public class DefaultValue : IImplementation
    {
        private TypedConstant DefaultValueProperty { get; set; }
        private IFieldSymbol FieldSymbol { get; set; }
        private ISymbol AttributeSymbol { get; set; }
        private INamedTypeSymbol ClassSymbol { get; set; }

        public void Initialize(TypedConstant nameProperty, IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
        {
            this.DefaultValueProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrDefaultValue);
            this.FieldSymbol = fieldSymbol;
            this.AttributeSymbol = attributeSymbol;
            this.ClassSymbol = classSymbol;
        }

        public bool SetterImplemented()
        {
            return false;
        }

        public string ProcessBindableParameters()
        {
            var fieldType = this.FieldSymbol.Type;
            var defaultValue = this.DefaultValueProperty.GetValue<string>(value =>
            {
                if (value != null)
                {
                    if (fieldType.IsStringType())
                    {
                        if (value == string.Empty)
                        {
                            return "string.empty";
                        }

                        return $"\"{value}\"";
                    }

                    return value;
                }

                return default;
            });

            return defaultValue != null ? $"defaultValue: {defaultValue}" : $"defaultValue: default({fieldType})";
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
}
