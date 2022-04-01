using Maui.BindableProperty.Generator.Core.BindableProperty;

namespace Maui.BindableProperty.Generator.Demo.Controls
{
    public partial class CustomEntry: ContentView
    {
        [AutoBindable]
        private string _placeholder;


        [AutoBindable(PropertyName = "Text")]
        private string _t;
    }
}
