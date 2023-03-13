using Maui.BindableProperty.Generator.Core.BindableProperty;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Maui.BindableProperty.Generator.Tests.Helpers;

public static class TestHelper
{
    public static Task Verify(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        IEnumerable<PortableExecutableReference> references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: references); // Pass the references to the compilation


        // Create an instance of our EnumGenerator incremental source generator
        var generator = new AutoBindablePropertyGenerator();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        var settings = new VerifySettings();
        settings.UseDirectory(@"..\Tests\VerifyFiles");

        // Use verify to snapshot test the source generator output!
        return Verifier.Verify(driver, settings: settings);
    }
}
