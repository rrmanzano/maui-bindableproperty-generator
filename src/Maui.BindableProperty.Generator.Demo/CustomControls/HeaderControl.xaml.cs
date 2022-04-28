using Maui.BindableProperty.Generator.Core;

namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl : ContentView
    {
        #pragma warning disable CS0169

        [AutoBindable(OnChanged = nameof(UpdateDisplayName))]
        private readonly string _firstName;

        [AutoBindable(PropertyName = "LastName", OnChanged = nameof(UpdateDisplayName))]
        private readonly string _l;

        [AutoBindable(DefaultValue = "DateTime.Now", OnChanged = nameof(OnDateTimeChanged))]
        private readonly DateTime _birthDate;

        [AutoBindable(DefaultValue = "USA")]
        private readonly string _country;

        [AutoBindable]
        private readonly string _displayName;

        [AutoBindable(HidesUnderlyingProperty = true, DefaultValue = "Color.FromArgb(\"#bdbde6\")")]
        private readonly Color _backgroundColor;

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

        private void UpdateDisplayName(string newValue)
        {
            // Do stuff here
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
    }
}