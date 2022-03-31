using Maui.BindableProperty.Generator.Core.BindableProperty;

namespace Maui.BindableProperty.Generator.Demo.Controls
{
    public partial class CustomButton: Button
    {
        [AutoBindable]
        private string _placeholder;
    }
}
