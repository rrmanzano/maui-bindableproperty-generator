using Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces;
using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation
{
    public class DefaultBindingMode : IImplementation
    {
        private const string FullNameSpaceBindingMode = $"{AutoBindableConstants.FullNameMauiControls}.BindingMode";
        private TypedConstant DefaultBindingModeValueProperty { get; set; }
        private IFieldSymbol FieldSymbol { get; set; }

        public DefaultBindingMode(IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
        {
            this.DefaultBindingModeValueProperty = fieldSymbol.GetTypedConstant(attributeSymbol, AutoBindableConstants.AttrDefaultBindingMode);
            this.FieldSymbol = fieldSymbol;
        }

        public bool SetterImplemented()
        {
            return false;
        }

        public string ProcessBindableParameters()
        {
            var fieldType = this.FieldSymbol.Type;
            var defaultValue = this.DefaultBindingModeValueProperty.GetValue<string>(value =>
            {
                if (!value.Contains(FullNameSpaceBindingMode))
                {
                    return $"{FullNameSpaceBindingMode}.{value}";
                }

                return value;
            });

            return defaultValue != default ? $"defaultBindingMode: {defaultValue}" : default;
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
