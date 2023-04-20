using System.Windows;
using System.Windows.Controls;
using CookBook.WPF.ViewModels;
using CookBook.Models.Models;
using CookBook.ApiClient.Repositories;
namespace CookBook.WPF.Views
{
    public partial class LoginView : UserControl
    {
        private LoginViewModel loginViewModel;
        public LoginView()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel( new GenericAPIRepository<User>("api/Token") );
            this.DataContext = loginViewModel;
            if(App.currentUser!=null)
            {
                LoggedIn();
            }
        }
            public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {  
            loginViewModel.Username = UserName.Text;
            loginViewModel.Password = password.Password;
            loginViewModel.ErrorMessages = "Beléptetés";
            LoggedIn();
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.Authentication = false;
            loginViewModel.ErrorMessages = "";
            App.currentUser = null;
            Buttons.Visibility = Visibility.Visible;
            signedIn.Visibility = Visibility.Hidden;
            wrongInput.Visibility = Visibility.Hidden;
            password.Clear();
            UserName.Clear();
        }
        public void LoggedIn()
        {
        if(loginViewModel.ErrorMessages==".")
            {
                loginViewModel.ErrorMessages = "Ön sikeresen belépett";
            }
                Buttons.Visibility = Visibility.Hidden;
                signedIn.Visibility = Visibility.Visible;
        }
    }
}  
