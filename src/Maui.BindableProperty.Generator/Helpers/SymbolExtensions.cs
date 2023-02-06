using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Helpers;

internal static class SymbolExtensions
{
    public static TypedConstant GetTypedConstant(
        this IFieldSymbol fieldSymbol,
        ISymbol attributeSymbol,
        string key)
    {
        // Get the AutoNotify attribute from the field, and any associated data
        var attributeData = fieldSymbol.GetAttributes().Single(ad => attributeSymbol.Equals(ad.AttributeClass, SymbolEqualityComparer.Default));
        return attributeData.NamedArguments.SingleOrDefault(kvp => kvp.Key == key).Value;
    }

    // Based on https://github.com/dotnet/roslyn/blob/main/src/Compilers/CSharp/Portable/Symbols/TypeSymbolExtensions.cs
    public static bool IsStringType(this ITypeSymbol type)
    {
        return type.SpecialType == SpecialType.System_String;
    }

    public static T GetValue<T>(
        this IFieldSymbol fieldSymbol,
        ISymbol attributeSymbol,
        string key,
        Func<T, T> onSuccess = null)
    {
        var typeConstant = fieldSymbol.GetTypedConstant(attributeSymbol, key);
        return typeConstant.GetValue(onSuccess);
    }

    public static T GetValue<T>(
        this TypedConstant TypedConstant,
        Func<T, T> onSuccess = null)
    {
        if (!TypedConstant.IsNull)
        {

            var value = TypedConstant.Value;
            if (value?.GetType() == typeof(T))
            {
                if (onSuccess != null)
                {
                    return onSuccess.Invoke((T)value);
                }

                return (T)value;
            }
        }

        return default;
    }
}
