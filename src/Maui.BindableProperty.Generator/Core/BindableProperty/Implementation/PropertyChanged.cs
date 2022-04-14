using Maui.BindableProperty.Generator.Helpers;
using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation
{
    public class PropertyChanged : IImplementation
    {
        private TypedConstant NameProperty { get; set; }
        private TypedConstant OnChangedProperty { get; set; }
        private IFieldSymbol FieldSymbol { get; set; }
        private ISymbol AttributeSymbol { get; set; }
        private INamedTypeSymbol ClassSymbol { get; set; }

        public void Initialize(TypedConstant nameProperty, IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
        {
            this.NameProperty = nameProperty;
            this.OnChangedProperty = fieldSymbol.GetTypedConstant(attributeSymbol, "OnChanged");
            this.FieldSymbol = fieldSymbol;
            this.AttributeSymbol = attributeSymbol;
            this.ClassSymbol = classSymbol;
        }

        public bool Implemented()
        {
            return !this.OnChangedProperty.IsNull;
        }

        public string ProcessBindableParameters()
        {
            if (!this.OnChangedProperty.IsNull)
            {
                var method = this.OnChangedProperty.Value?.ToString();
                return $@"propertyChanged: __{method}";
            }

            return null;
        }

        public void ProcessBodyStter(CodeWriter w)
        {
            if (!this.OnChangedProperty.IsNull)
            {
                var method = this.OnChangedProperty.Value?.ToString();
                w._($@"this.{method}(value);");
            }
        }

        public void ProcessImplementationLogic(CodeWriter w)
        {
            if (!this.OnChangedProperty.IsNull)
            {
                var method = this.OnChangedProperty.Value?.ToString();
                w._(AutoBindableConstants.AttributeGeneratedCodeString);
                using (w.B(@$"public static void __{method}(Microsoft.Maui.Controls.BindableObject bindable, object oldValue, object newValue)"))
                {
                    w._($@"(({this.ClassSymbol.Name})bindable).{method}(({this.FieldSymbol.Type})newValue);");
                }
            }
        }
    }
}
