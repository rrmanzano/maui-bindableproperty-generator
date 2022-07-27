using Maui.BindableProperty.Generator.Core.BindableProperty;

namespace Maui.BindableProperty.Generator.Helpers;

public static class AttributeBuilder
{
    public static string GetAttrGeneratedCodeString()
    {
        var version = typeof(AutoBindablePropertyGenerator).Assembly.GetName().Version.ToString();
        var genertorFullName = typeof(AutoBindablePropertyGenerator).FullName;
        var generatedCodeString = AutoBindableConstants.AttrGeneratedCodeString.Replace("{GeneratorFullName}", genertorFullName).Replace("{Version}", version);
        return generatedCodeString;
    }

    public static void WriteAllAttrGeneratedCodeStrings(CodeWriter w)
    {
        w._(GetAttrGeneratedCodeString());
        w._(AutoBindableConstants.AttrExcludeFromCodeCoverageString);
    }
}