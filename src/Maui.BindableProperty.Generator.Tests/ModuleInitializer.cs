using System.Runtime.CompilerServices;

namespace Maui.BindableProperty.Generator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}