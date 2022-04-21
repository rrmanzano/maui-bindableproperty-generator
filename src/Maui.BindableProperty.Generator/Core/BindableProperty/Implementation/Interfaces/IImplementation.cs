using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core.BindableProperty.Implementation.Interfaces
{
    public interface IImplementation
    {
        void Initialize(TypedConstant nameProperty, IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol);
        bool SetterImplemented();
        string ProcessBindableParameters();
        void ProcessBodyStter(CodeWriter w);
        void ProcessImplementationLogic(CodeWriter w);
    }
}
