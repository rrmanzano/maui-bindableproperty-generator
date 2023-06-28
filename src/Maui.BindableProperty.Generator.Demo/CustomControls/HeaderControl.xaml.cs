using Maui.BindableProperty.Generator.Core;

namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl : VerticalStackLayout
    {
        private static bool ValidateNotNull(BindableObject _, object value) => value != null;

        private bool ValidateIsNullOrEmpty(BindableObject _, string? value)
        {
            // Do stuff here
            return !string.IsNullOrEmpty(value);
        }

        #pragma warning disable CS0169

        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName = string.Empty;

        [AutoBindable()]
        private readonly string _placeholderName = string.Empty;

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

        partial void OnDisplayNameChanging(string? value)
        {
            System.Diagnostics.Debug.WriteLine("Method OnDisplayNameChanging fired");
            System.Diagnostics.Debug.WriteLine(value);
        }

        partial void OnDisplayNameChanged(string? value)
        {
            System.Diagnostics.Debug.WriteLine("Method OnDisplayNameChanged fired");
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}