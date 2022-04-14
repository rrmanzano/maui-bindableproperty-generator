using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Maui.BindableProperty.Generator.Demo.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private int _count = 0;

        [ObservableProperty]
        private string _countText;

        public MainViewModel()
        {
            this.CountText = "Count: 0";
        }

        [ICommand]
        private void CounterClicked()
        {
            _count += 20;
            this.CountText = $"Count: {_count}";
        }
    }
}
