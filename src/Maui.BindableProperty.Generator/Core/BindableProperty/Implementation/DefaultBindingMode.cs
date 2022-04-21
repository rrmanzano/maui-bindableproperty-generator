using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation
{
    public class DefaultBindingMode : IImplementation
    {
        private const string FullNameSpaceBindingMode = "Microsoft.Maui.Controls.BindingMode";
        private TypedConstant DefaultBindingModeValueProperty { get; set; }
        private IFieldSymbol FieldSymbol { get; set; }
        private ISymbol AttributeSymbol { get; set; }
        private INamedTypeSymbol ClassSymbol { get; set; }

        public void Initialize(TypedConstant nameProperty, IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
        {
            this.DefaultBindingModeValueProperty = fieldSymbol.GetTypedConstant(attributeSymbol, "DefaultBindingMode");
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
            var defaultValue = this.DefaultBindingModeValueProperty.Validate(value =>
            {
                if (!value.Contains(FullNameSpaceBindingMode))
                {
                    return $"{FullNameSpaceBindingMode}.{value}";
                }

                return value;
            });

            return defaultValue != default ? $"defaultBindingMode: {defaultValue}" : default;
        }

        public void ProcessBodyStter(CodeWriter w)
        {
            // Not implemented
        }

        public void ProcessImplementationLogic(CodeWriter w)
        {
            // Not implemented
        }
    }
}
