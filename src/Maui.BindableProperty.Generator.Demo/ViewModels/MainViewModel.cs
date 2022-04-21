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

        [ICommand]
        private void SaveClicked()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Country = null;
            this.BirthDate = DateTime.Now;
        }

        partial void OnFirstNameChanged(string value)
        {
            System.Diagnostics.Debug.WriteLine("Method OnFirstNameChanged fired");
            System.Diagnostics.Debug.WriteLine(value);
        }
    }
}
