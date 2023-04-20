using System.Threading.Tasks;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using CookBook.ApiClient.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.WPF.ViewModels
{
    public class LoginViewModel : PagerViewModel
    {
        protected IGenericRepository<User?> _loginRepo;
        public bool _authentication;
        public bool Authentication
        {
            get {return _authentication;}
            set
            {
                _authentication = value;
                OnPropertyChanged(nameof(Authentication));
            }
        }
        public string? _errorMessages;
        public string ErrorMessages
        {
            get {return _errorMessages; }
            set
            {
                _errorMessages = value;
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }
        private string? _password;
        private string? _username;
        public string? Username
        {
            get { return _username;}
            set
            {
                _username = value;
                LoginCommand.NotifyCanExecuteChanged();    
            }
        }
        public string? Password
        {
            get { return _password;   }
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.NotifyCanExecuteChanged();    
            }
        }
        public RelayCommand LoginCommand { get; set; }
        private UserLogins log = new UserLogins();
        public LoginViewModel(IGenericRepository<User> LoginRepo)
        {
          if (App.currentUser!=null) Authentication= true;
            LoginCommand = new RelayCommand(() => Login());
            if (LoginRepo != null)
            {
                _loginRepo = LoginRepo;
                ErrorMessages = ".";
            }
        }  
        protected override async Task LoadData()
        {
            if (Username !="" && Password != "")
            {
                ErrorMessages += ".";   
                App.currentUser= await _loginRepo.GetLoginAsync(log);
                if (App.currentUser == null)
                {
                    ErrorMessages = "Rossz felhasználónév vagy jelszó";
                    Authentication = false;   
                }
                if (App.currentUser!=null&&App.currentUser.role != "Admin")
                {
                    ErrorMessages = "Ez a platform nem elérhető csak admin számára";
                    Authentication = false;
                }
                if (App.currentUser != null && App.currentUser.role == "Admin")
                {
                    ErrorMessages = "Sikeres belépés";
                    Authentication = true;
                    App.Current.Services.GetRequiredService<MainViewModel>();
                    return;
                }
            }
            else if (Username == "")
            {
                ErrorMessages = "Nem adott meg Felhasználó nevet.";
            }
            else if (Password== "")
            {
                ErrorMessages = "Nem adott meg Jelszót nevet.";
            }
           else ErrorMessages = "Ellenőrizze hogy fut-e\n az adadbázis!?";
        }
        private async void Login()
        {
            log.Username = _username;
            log.Password = Password;
            await Task.Run(LoadData);

            List<string> valami = new List<string>
            {
                "",
                ""
            };
        }
    }
}




    



