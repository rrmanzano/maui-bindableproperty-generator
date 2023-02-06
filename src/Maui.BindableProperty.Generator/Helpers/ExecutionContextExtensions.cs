using Microsoft.CodeAnalysis;
using Maui.BindableProperty.Generator.Core;

namespace Maui.BindableProperty.Generator.Helpers;

internal static class ExecutionContextExtensions
{
    public static void EachClass<T>(
        this GeneratorExecutionContext context,
        string metadataName,
        Action<INamedTypeSymbol, IGrouping<INamedTypeSymbol, IFieldSymbol>> action) where T : IFieldSyntaxContextReceiver
    {
        // Retrieve the populated receiver 
        if (context.SyntaxContextReceiver is not T receiver)
            return;

        if (receiver is null)
            return;

        // Get the added attribute
        var attributeSymbol = context.Compilation?.GetTypeByMetadataName(metadataName);

        // Group the fields by class, and generate the source
#pragma warning disable RS1024 // Symbols should be compared for equality
        foreach (var group in receiver.Fields?.GroupBy(f => f.ContainingType) ?? Enumerable.Empty<IGrouping<INamedTypeSymbol, IFieldSymbol>>())
        {
            action?.Invoke(attributeSymbol, group);
        }
#pragma warning restore RS1024 // Symbols should be compared for equality
    }
}
