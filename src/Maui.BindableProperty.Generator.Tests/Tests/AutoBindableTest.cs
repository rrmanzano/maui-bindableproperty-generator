using Maui.BindableProperty.Generator.Tests.Helpers;

namespace Maui.BindableProperty.Generator.Tests.Tests;

[UsesVerify]
public class AutoBindableTest
{
    private const string ClassBase = """
    using Maui.BindableProperty.Generator.Core;

    namespace Maui.BindableProperty.Generator.Demo.CustomControls
    {
        {0} partial class HeaderControl
        {
            {1}
        }
    }
    """;

    [Fact]
    public Task Create_Without_Parameters()
    {
        var definition = """
        [AutoBindable]
        private readonly string _firstName = string.Empty;
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_PropertyName()
    {
        var definition = """
        [AutoBindable(PropertyName = "LastName")]
        private readonly string? _l;
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_OnChanged_Event()
    {
        var definition = """
        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName = string.Empty;

        private void UpdateDisplayName()
        {
            // Do stuff here
        }
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_DefaultValue()
    {
        var definition = """
        [AutoBindable(DefaultValue = "DateTime.Now", OnChanged = nameof(OnDateTimeChanged))]
        private readonly DateTime? _birthDate;

        private void OnDateTimeChanged()
        {
            // Do stuff here
        }
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_ValidateValue()
    {
        var definition = """
        [AutoBindable(DefaultValue = "USA", ValidateValue = nameof(ValidateIsNullOrEmpty))]
        private readonly string? _country;

        private bool ValidateIsNullOrEmpty(BindableObject _, string? value)
        {
            // Do stuff here
            return !string.IsNullOrEmpty(value);
        }
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_ValidateValue_Static()
    {
        var definition = """
        [AutoBindable(ValidateValue = nameof(ValidateNotNull))]
        private readonly string? _zipCode;

        private static bool ValidateNotNull(BindableObject _, object value) => value != null;
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_PropertyAccessibility()
    {
        var definition = """
        [AutoBindable(PropertyAccessibility = BindablePropertyAccessibility.ProtectedOrInternal)]
        private readonly string? _age;
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_With_PropertyAccessibility_Internal()
    {
        var definition = """
        [AutoBindable]
        private readonly string? _age;
        """;

        return Build(definition, "internal");
    }

    [Fact]
    public Task Create_With_HidesUnderlyingProperty()
    {
        var definition = """
        [AutoBindable(HidesUnderlyingProperty = true, DefaultValue = "Color.FromArgb(\"#cc3340\")")]
        private readonly Color _backgroundColor = Color.FromArgb("#cc3340");
        """;

        return Build(definition);
    }

    [Fact]
    public Task Create_AllOptions()
    {
        var definition = """
        private static bool ValidateNotNull(BindableObject _, object value) => value != null;

        private bool ValidateIsNullOrEmpty(BindableObject _, string? value)
        {
            // Do stuff here
            return !string.IsNullOrEmpty(value);
        }

        #pragma warning disable CS0169

        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName = string.Empty;

        [AutoBindable(PropertyName = "LastName", OnChanged = nameof(UpdateDisplayName))]
        private readonly string? _l;

        [AutoBindable(DefaultValue = "DateTime.Now", OnChanged = nameof(OnDateTimeChanged))]
        private readonly DateTime? _birthDate;

        [AutoBindable(DefaultValue = "USA", ValidateValue = nameof(ValidateIsNullOrEmpty))]
        private readonly string? _country;

        [AutoBindable(ValidateValue = nameof(ValidateNotNull))]
        private readonly string? _zipCode;

        [AutoBindable]
        private readonly string? _displayName;

        [AutoBindable(PropertyAccessibility = BindablePropertyAccessibility.ProtectedOrInternal)]
        private readonly string? _age;

        [AutoBindable(HidesUnderlyingProperty = true, DefaultValue = "Color.FromArgb(\"#cc3340\")")]
        private readonly Color _backgroundColor = Color.FromArgb("#cc3340");

        #pragma warning restore CS0169

        public HeaderControl()
        {
            InitializeComponent();
        }

        private void UpdateDisplayName()
        {
            // Do stuff here
            var name = string.Empty;
            if (!string.IsNullOrEmpty(this.FirstName))
            {
                name = FirstName;
            }

            if (!string.IsNullOrEmpty(this.LastName))
            {
                name = $"{name} {this.LastName}";
            }

            this.DisplayName = name.Trim();
        }

        private void UpdateDisplayName(string oldValue, string newValue)
        {
            // Do stuff here
        }

        private void OnDateTimeChanged()
        {
            // Do stuff here
            System.Diagnostics.Debug.WriteLine("Method OnDateTimeChanged fired - No parameters");
        }

        private void OnDateTimeChanged(DateTime oldValue, DateTime newValue)
        {
            // Do stuff here
            System.Diagnostics.Debug.WriteLine("Method OnDateTimeChanged fired - Two Paramter");
            System.Diagnostics.Debug.WriteLine($"{oldValue} - {newValue}");
        }

        private void OnDateTimeChanged(string oldValue)
        {
            // This method never will fired becuse the parameter is a different type
        }
        """;

        return Build(definition);
    }

    private Task Build(string definition, string declaredAccessibility = "public")
    {
        var source =
                ClassBase
                    .Replace("{0}", declaredAccessibility)
                    .Replace("{1}", definition);

        // Pass the source code to our helper and snapshot test the output
        return TestHelper.Verify(source);
    }
}