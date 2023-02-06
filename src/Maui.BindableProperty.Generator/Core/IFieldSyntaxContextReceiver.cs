using Microsoft.CodeAnalysis;

namespace Maui.BindableProperty.Generator.Core;

internal interface IFieldSyntaxContextReceiver
{
    List<IFieldSymbol> Fields { get; }
}
