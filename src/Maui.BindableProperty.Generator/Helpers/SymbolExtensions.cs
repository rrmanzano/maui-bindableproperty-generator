using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Helpers
{
    internal static class SymbolExtensions
    {
        public static TypedConstant GetTypedConstant(
            this IFieldSymbol fieldSymbol,
            ISymbol attributeSymbol,
            string key)
        {
            // Get the AutoNotify attribute from the field, and any associated data
            var attributeData = fieldSymbol.GetAttributes().Single(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));
            return attributeData.NamedArguments.SingleOrDefault(kvp => kvp.Key == key).Value;
        }
    }
}
