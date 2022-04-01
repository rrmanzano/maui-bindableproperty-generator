using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Maui.BindableProperty.Generator.Helpers;

namespace Maui.BindableProperty.Generator.Core.BindableProperty
{

    [Generator]
    public class AutoBindablePropertyGenerator : ISourceGenerator
    {

        private const string attributeText = @"
        using System;
        namespace Maui.BindableProperty.Generator.Core.BindableProperty
        {
            [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
            [System.Diagnostics.Conditional(""AutoBindableGenerator_DEBUG"")]
            public sealed class AutoBindableAttribute : Attribute
            {
                public AutoBindableAttribute(){}

                public string PropertyName { get; set; }
            }
        }";

        public void Execute(GeneratorExecutionContext context)
        {
            context.EachField<AutoBindableSyntaxReceiver>(AutoBindableConstants.AttributeClassDisplayString, (attributeSymbol, group) => {
                var classSource = this.ProcessClass(group.Key, group.ToList(), attributeSymbol, context);
                context.AddSource($"{group.Key.Name}.generated.cs", SourceText.From(classSource, Encoding.UTF8));
            });
        }

        private string ProcessClass(INamedTypeSymbol classSymbol, List<IFieldSymbol> fields, ISymbol attributeSymbol, GeneratorExecutionContext context)
        {
            if (!classSymbol.ContainingSymbol.Equals(classSymbol.ContainingNamespace, SymbolEqualityComparer.Default))
            {
                return null; // TODO: issue a diagnostic that it must be top level
            }

            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            var w = new CodeWriter(CodeWriterSettings.CSharpDefault);
            using (w.B(@$"namespace {namespaceName}"))
            {
                w._(AutoBindableConstants.AttributeGeneratedCodeString);
                using (w.B(@$"public partial class {classSymbol.Name}"))
                {
                    // Create properties for each field 
                    foreach (IFieldSymbol fieldSymbol in fields)
                    {
                        this.ProcessBindableProperty(w, fieldSymbol, attributeSymbol, classSymbol);
                    }
                }
            }

            return w.ToString();
        }

        private void ProcessBindableProperty(CodeWriter w, IFieldSymbol fieldSymbol, ISymbol attributeSymbol, INamedTypeSymbol classSymbol)
        {
            // Get the name and type of the field
            var fieldName = fieldSymbol.Name;
            var fieldType = fieldSymbol.Type;

            // Get the AutoNotify attribute from the field, and any associated data
            var attributeData = fieldSymbol.GetAttributes().Single(ad => ad.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));
            var overridenNameOpt = attributeData.NamedArguments.SingleOrDefault(kvp => kvp.Key == "PropertyName").Value;

            var propertyName = this.ChooseName(fieldName, overridenNameOpt);
            if (propertyName?.Length == 0 || propertyName == fieldName)
            {
                // TODO: issue a diagnostic that we can't process this field
                return;
            }

            var bindablePropertyName = $@"{propertyName}Property";
            w._(AutoBindableConstants.AttributeGeneratedCodeString);
            w._($@"public static readonly Microsoft.Maui.Controls.BindableProperty {bindablePropertyName} = Microsoft.Maui.Controls.BindableProperty.Create(nameof({propertyName}), typeof({fieldType}), typeof({classSymbol.Name}), default({fieldType}));");
            w._(AutoBindableConstants.AttributeGeneratedCodeString);
            using (w.B(@$"public {fieldType} {propertyName}"))
            {
                w._($@"get => ({fieldType})GetValue({bindablePropertyName});",
                    $@"set => SetValue({bindablePropertyName}, value);");
            }
        }

        private string ChooseName(string fieldName, TypedConstant overridenNameOpt)
        {
            if (!overridenNameOpt.IsNull)
            {
                return overridenNameOpt.Value?.ToString();
            }

            fieldName = fieldName.TrimStart('_');
            if (fieldName.Length == 0)
                return string.Empty;

            if (fieldName.Length == 1)
                return fieldName.ToUpper();

            return fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Register the attribute source
            context.RegisterForPostInitialization((i) => i.AddSource("AutoBindableAttribute", attributeText));

            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new AutoBindableSyntaxReceiver());
        }
    }
}
