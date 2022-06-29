using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Maui.BindableProperty.Generator.Demo.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _firstName;

        [ObservableProperty]
        private string _lastName;

        [ObservableProperty]
        private DateTime _birthDate;

        [ObservableProperty]
        private string _country;

        public MainViewModel(){}

        [RelayCommand]
        private void LogClicked()
        {
            System.Diagnostics.Debug.WriteLine(@$"FirstName -> {this.FirstName}");
            System.Diagnostics.Debug.WriteLine(@$"LastName -> {this.LastName}");
            System.Diagnostics.Debug.WriteLine(@$"Country -> {this.Country}");
            System.Diagnostics.Debug.WriteLine(@$"BirthDate -> {this.BirthDate}");
        }

        partial void OnFirstNameChanged(string value)
        {
            System.Diagnostics.Debug.WriteLine("Method OnFirstNameChanged fired");
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
