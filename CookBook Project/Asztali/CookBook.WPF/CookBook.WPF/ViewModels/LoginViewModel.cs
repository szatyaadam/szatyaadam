using CookBook.WPF.Commands;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using CookBook.WPF.Views;
using CookBook.ApiClient.Models;
using System.Security;
using System;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace CookBook.WPF.ViewModels
{
    public class LoginViewModel : PagerViewModel
    {
        private MainViewModel mainViewModel;

        protected IGenericRepository<User> _loginRepo;
        public bool _authentication;
        public bool Authentication
        {
            get
            {
                return _authentication;
            }
            set
            {
                _authentication = value;
                OnPropertyChanged(nameof(Authentication));
            }
        }
        public string _errorMessages;
        public string ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
            set
            {
                _errorMessages = value;
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }
        private string _password;
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                LoginCommand.NotifyCanExecuteChanged();    
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
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
          if (App.currentUser!=null)
            {
                Authentication= true;
            }
            LoginCommand = new RelayCommand(() => Login());
            if (LoginRepo != null)
            {
                _loginRepo = LoginRepo;
                
            }
            
            ErrorMessages += ".";
        }  
        protected override async Task LoadData()
        {
            
            if (Username !="" && Password != "")
            {
                ErrorMessages += ".";   
                
                App.currentUser= await _loginRepo.GetLoginAsync(log);
                
                
                if (App.currentUser == null)
                {
                    ErrorMessages = "Wrong UserName or Password";
                    Authentication = false;   
                    
                 
                }
                if (App.currentUser!=null&&App.currentUser.role != "Admin")
                {
                    ErrorMessages = "This platform is not available for your role.";
                    Authentication = false;
                }
                if (App.currentUser != null && App.currentUser.role == "Admin")
                    {
                       ErrorMessages = "Success Sign in ";
                       Authentication = true; 
                       return;
                    }
            
               
            }

           else ErrorMessages = "Wrong Sign in ";
        }
        private async void Login()
        {
            log.Username = _username;
            log.Password = Password;
            Task.Run(LoadData);
        }
         
    }
}




    



