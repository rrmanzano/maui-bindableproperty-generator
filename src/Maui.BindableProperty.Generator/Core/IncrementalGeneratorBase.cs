using Maui.BindableProperty.Generator.Core.BindableProperty;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Maui.BindableProperty.Generator.Core;

public abstract class IncrementalGeneratorBase
{
    protected void Initialize(
        IncrementalGeneratorInitializationContext context,
        SourceText sourceText,
        Action<INamedTypeSymbol, IGrouping<INamedTypeSymbol, IFieldSymbol>, SourceProductionContext> action
    )
    {
        // Add the marker attribute
        context.RegisterPostInitializationOutput((ctx) => ctx.AddSource(
            AutoBindableConstants.AttrName,
            sourceText));

        // Do a simple filter for fields
        var fieldsDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // Select fields with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // Select the fields with the [AutoBindable] attribute
            .Where(static m => m is not null)!; // Filter out attributed fields that we don't care about

        // Combine the selected fields with the 'Compilation'
        var compilationAndFields = context.CompilationProvider.Combine(fieldsDeclarations.Collect());

        // Generate the source using the compilation and fields
        context.RegisterSourceOutput(compilationAndFields,
            (spc, source) =>
            {
                var compilation = source.Item1;
                var fields = source.Item2;

                if (fields.IsDefaultOrEmpty)
                {
                    // Nothing to do yet
                    return;
                }

                var distinctFields = fields.Distinct();

                // Convert each FieldDeclarationSyntax to an IFieldSymbol
                var fieldsToGenerate = this.GetTypesToGenerate(compilation, distinctFields, spc.CancellationToken);

                if (fieldsToGenerate.Count > 0)
                {
                    // Generate the source code and add it to the output
                    var attributeSymbol = compilation.GetTypeByMetadataName(AutoBindableConstants.AttrClassDisplayString);

                    // Group the fields by class, and generate the source
                    #pragma warning disable RS1024 // Symbols should be compared for equality
                    foreach (var group in fieldsToGenerate.GroupBy(f => f.ContainingType) ?? Enumerable.Empty<IGrouping<INamedTypeSymbol, IFieldSymbol>>())
                    {
                        action?.Invoke(attributeSymbol, group, spc);
                    }
                    #pragma warning restore RS1024 // Symbols should be compared for equality
                }
            });
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
            => node is FieldDeclarationSyntax m && m.AttributeLists.Count > 0;

    private static FieldDeclarationSyntax GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // We know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var fieldsDeclarationSyntax = (FieldDeclarationSyntax)context.Node;

        // Loop through all the attributes
        foreach (VariableDeclaratorSyntax variable in fieldsDeclarationSyntax.Declaration.Variables)
        {
            // Get the symbol being declared by the field, and keep it if its annotated
            if (context.SemanticModel.GetDeclaredSymbol(variable) is not IFieldSymbol fieldSymbol)
            {
                // Weird, we couldn't get the symbol, ignore it
                continue;
            }

            // Is the attribute the [AutoBindableAttribute] attribute?
            if (fieldSymbol.GetAttributes().Any(ad => ad?.AttributeClass?.ToDisplayString() == AutoBindableConstants.AttrClassDisplayString))
            {
                // Return the class
                return fieldsDeclarationSyntax;
            }
        }

        // We didn't find the attribute we were looking for
        return null;
    }

    private List<IFieldSymbol> GetTypesToGenerate(
        Compilation compilation,
        IEnumerable<FieldDeclarationSyntax> fields,
        CancellationToken ct)
    {
        // Create a list to hold our output
        var fieldsToGenerate = new List<IFieldSymbol>();

        // Get the semantic representation of our marker attribute 
        var fieldAttribute = compilation.GetTypeByMetadataName(AutoBindableConstants.AttrClassDisplayString);

        if (fieldAttribute == null)
        {
            // If this is null, the compilation couldn't find the marker attribute type
            // which suggests there's something very wrong! Bail out..
            return fieldsToGenerate;
        }

        foreach (FieldDeclarationSyntax fieldDeclarationSyntax in fields)
        {
            // Stop if we're asked to
            ct.ThrowIfCancellationRequested();

            // Get the semantic representation of the field syntax
            foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
            {
                // Get the symbol being declared by the field, and keep it if its annotated
                SemanticModel semanticModel = compilation.GetSemanticModel(fieldDeclarationSyntax.SyntaxTree);
                if (semanticModel.GetDeclaredSymbol(variable) is not IFieldSymbol fieldSymbol)
                {
                    // Something went wrong
                    continue;
                }

                fieldsToGenerate.Add(fieldSymbol);
            }
        }

        return fieldsToGenerate;
    }
}
