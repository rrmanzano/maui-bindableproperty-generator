using Maui.BindableProperty.Generator.Core;


namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl : ContentView
    {
        [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWayToSource), OnChanged = nameof(UpdateDisplayName))]
        private string _firstName;

        [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWayToSource), PropertyName = "LastName", OnChanged = nameof(UpdateDisplayName))]
        private string _l;

        [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWayToSource), DefaultValue = "DateTime.Now", OnChanged = nameof(OnDateTimeChanged))]
        private DateTime _birthDate;

        [AutoBindable(DefaultBindingMode = nameof(BindingMode.OneWayToSource), DefaultValue = "USA")]
        private string _country;

        [AutoBindable]
        private string _displayName;

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

            if (!string.IsNullOrEmpty(LastName))
            {
                name = $"{name} {LastName}";
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