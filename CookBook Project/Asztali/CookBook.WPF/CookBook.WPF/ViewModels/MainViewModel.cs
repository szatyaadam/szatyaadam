using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.ApiClient.Models;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Security.Authentication;

namespace CookBook.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
 

        private ObservableObject _selectedObject;
        public ObservableObject SelectedObject
        {
            get { return _selectedObject; }
            set { SetProperty(ref _selectedObject, value); }
        }

        public IRelayCommand<object> UpdateViewCommand { get; set; }

        public MainViewModel()
        { 
            UpdateViewCommand = new RelayCommand<object>(e => Execute(e));
            SelectedObject = App.Current.Services.GetRequiredService<LoginViewModel>();

        }


        private void Execute(object parameter)
        {
            if (parameter.ToString() == "Meals")
            {
                SelectedObject = App.Current.Services.GetRequiredService<MealsViewModel>();
              
            }
            else if (parameter.ToString() == "User")
            {
                
                SelectedObject = App.Current.Services.GetRequiredService<UserViewModel>();
              
            }
            else if (parameter.ToString() == "Login")
            {
                SelectedObject = App.Current.Services.GetRequiredService<LoginViewModel>();
            }
        }

    }

}
