using Maui.BindableProperty.Generator.Core;

namespace Maui.BindableProperty.Generator.Demo.CustomControls
{
    public partial class HeaderGenericControl<T> : VerticalStackLayout
    {
        #pragma warning disable CS0169

        [AutoBindable]
        private bool _isGenerated;

        [AutoBindable(OnChanged = nameof(UpdateGenericSample))]
        private readonly T _genericSample = default!;

        #pragma warning restore CS0169

        public HeaderGenericControl()
        {
            this.IsGenerated = false;
        }

        private void UpdateGenericSample()
        {
            // Do stuff here
        }

        private void UpdateGenericSample(T oldValue, T newValue)
        {
            // Do stuff here
        }
    }
}