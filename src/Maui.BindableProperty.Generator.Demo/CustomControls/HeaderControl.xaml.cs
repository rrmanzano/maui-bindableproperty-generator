using Maui.BindableProperty.Generator.Core;


namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderControl : ContentView
    {
        [AutoBindable]
        public string _H1LabelText;

        [AutoBindable]
        public string _H2LabelText;

        [AutoBindable(OnChanged = nameof(OnH3LabelTextChanged))]
        public string _H3LabelText;

        public HeaderControl()
        {
            InitializeComponent();
        }

        private void OnH3LabelTextChanged(string newValue)
        {
            // Do stuff here
        }
    }
}