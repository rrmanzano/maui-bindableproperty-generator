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

        // Based on https://github.com/dotnet/roslyn/blob/main/src/Compilers/CSharp/Portable/Symbols/TypeSymbolExtensions.cs
        public static bool IsStringType(this ITypeSymbol type)
        {
            return type.SpecialType == SpecialType.System_String;
        }

        public static string Validate(this TypedConstant TypedConstant, Func<string, string> onSuccess)
        {
            if (!TypedConstant.IsNull)
            {
                var value = TypedConstant.Value?.ToString();
                return onSuccess.Invoke(value);
            }

            return default;
        }
    }
}
