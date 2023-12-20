namespace Maui.BindableProperty.Generator.Core.BindableProperty;

public class AutoBindableConstants
{
    public const string FullNameMauiControls = "Microsoft.Maui.Controls";

    public const string ProjectName = "Maui.BindableProperty.Generator";

    public const string ExceptionTitle = $"{ProjectName} exception";

    public const string ExceptionMBPG001Id = "MBPG001";

    public const string ExceptionMBPG001Message = "An exception was thrown trying to generate the partial class '{0}'.\nException: {1}";

    public const string ExceptionMBPG002Id = "MBPG002";

    public const string ExceptionMBPG002Message = $"{AutoBindableConstants.ProjectName} does not currently support nested classes. Class '{{0}}'";

    public const string ExceptionMBPG003Id = "MBPG003";

    public const string ExceptionMBPG003Message = "An exception was thrown trying to generate the bindable property of '{0}' in the class '{1}'. Exception: {2}";

    public const string ExceptionMBPG004Id = "MBPG004";

    public const string ExceptionMBPG004Message = "An exception was thrown trying to process this field '{0}'";

    public const string AttrName = "AutoBindableAttribute";

    public const string AttrClassDisplayString = @$"{ProjectName}.Core.{AttrName}";

    public const string AttrGeneratedCodeString = @$"[global::System.CodeDom.Compiler.GeneratedCode(""{{GeneratorFullName}}"", ""{{Version}}"")]";

    public const string AttrExcludeFromCodeCoverageString = "[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]";

    public const string AttrPropertyName = "PropertyName";

    public const string AttrOnChanged = "OnChanged";

    public const string AttrOnChanging = "OnChanging";

    public const string AttrDefaultValue = "DefaultValue";

    public const string AttrDefaultValueRaw = "DefaultValueRaw";

    public const string AttrValidateValue = "ValidateValue";

    public const string AttrDefaultBindingMode = "DefaultBindingMode";

    public const string AttrHidesUnderlyingProperty = "HidesUnderlyingProperty";

    public const string AttrPropertyAccessibility = "PropertyAccessibility";
}
