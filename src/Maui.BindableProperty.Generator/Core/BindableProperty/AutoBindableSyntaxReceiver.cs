using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Maui.BindableProperty.Generator.Core.BindableProperty;

public class AutoBindableSyntaxReceiver : ISyntaxContextReceiver, IFieldSyntaxContextReceiver
{
    public List<IFieldSymbol> Fields { get; } = new List<IFieldSymbol>();

    /// <summary>
    /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
    /// </summary>
    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        // any field with at least one attribute is a candidate for property generation
        if (context.Node is FieldDeclarationSyntax fieldDeclarationSyntax
            && fieldDeclarationSyntax.AttributeLists.Count > 0)
        {
            foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
            {
                // Get the symbol being declared by the field, and keep it if its annotated
                if (context.SemanticModel.GetDeclaredSymbol(variable) is IFieldSymbol fieldSymbol &&
                    fieldSymbol.GetAttributes().Any(ad => ad?.AttributeClass?.ToDisplayString() == AutoBindableConstants.AttrClassDisplayString))
                {
                    Fields.Add(fieldSymbol);
                }
            }
        }
    }
}
