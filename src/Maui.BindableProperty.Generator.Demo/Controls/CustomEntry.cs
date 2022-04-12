using Maui.BindableProperty.Generator.Core;

namespace Maui.BindableProperty.Generator.Demo.Controls
{
    public partial class CustomEntry: ContentView
    {
        [AutoBindable]
        private string _placeholder;


        [AutoBindable(OnChanged = nameof(OnTextChanged))]
        private string _text;

        private void OnTextChanged(string newValue)
        {
            // Do stuff here
        }
    }
}
