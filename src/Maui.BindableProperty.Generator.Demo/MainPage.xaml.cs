using Maui.BindableProperty.Generator.Demo.ViewModels;

namespace Maui.BindableProperty.Generator.Demo
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            this.BindingContext = viewModel;
            InitializeComponent();
        }
    }
}