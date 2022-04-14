using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation
{
    public interface IImplementation
    {
        void Initialize(TypedConstant nameProperty, IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol);
        bool Implemented();
        string ProcessBindableParameters();
        void ProcessBodyStter(CodeWriter w);
        void ProcessImplementationLogic(CodeWriter w);
    }
}
