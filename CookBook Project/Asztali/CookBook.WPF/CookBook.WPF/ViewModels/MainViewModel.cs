using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CookBook.ApiClient.Models;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CookBook.WPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        protected IGenericRepository<User> _refreshRepo;
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
            LoadData();
        }
        protected  async Task LoadData()
        {
            if (App.currentUser != null && App.IsRefreshing == true)
            {
                _refreshRepo = new GenericAPIRepository<User>("api/Token");
                JwtToken old = new JwtToken { Access_Token = App.currentUser.access_Token, Refresh_Token = App.currentUser.refresh_Token };
                var fresh = await _refreshRepo.GetRefreshTokenAsync(old);
                try
                {
                    App.currentUser.refresh_Token = fresh.refresh_Token;
                    App.currentUser.access_Token = fresh.access_Token;
                    App.IsRefreshing = false;
                    return;
                }
                catch(Exception ex) { MessageBox.Show($"{ex}", "error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
            private void Execute(object parameter)
            {
                if (App.currentUser== null|| parameter.ToString() == "Login")
                {
                    SelectedObject = App.Current.Services.GetRequiredService<LoginViewModel>();
                    LoadData();
                }
                 else if (parameter.ToString() == "Meals")
                 {
                    SelectedObject = App.Current.Services.GetRequiredService<MealsViewModel>();
                    LoadData();
                 }
                else if (parameter.ToString() == "User")
                {
                    SelectedObject = App.Current.Services.GetRequiredService<UserViewModel>();
                    LoadData();
                }  
            }
    }
}
