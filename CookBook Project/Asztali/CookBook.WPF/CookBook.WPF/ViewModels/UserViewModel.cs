using CookBook.ApiClient.Repositories;
using CommunityToolkit.Mvvm.Input;
using CookBook.WPF.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CookBook.Models.Models;
using CookBook.ApiClient.Models;
using System.Windows;
using System.Net.Http;


namespace CookBook.WPF.ViewModels
{
    public class UserViewModel : PagerViewModel , IEditCommand<User>
    {
        private JwtToken _jwt;
        public JwtToken Jwt
        {
            get { return _jwt; }
            set { SetProperty(ref _jwt, value);}
        }
        private IPagerRepository<User> _userRepo;
        private ObservableCollection<User> _users = new();
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value);}
        }
        private User  _selectedUser = new();
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }
        public IRelayCommand? NewCommand { get; set; }
        public IRelayCommand? NewItemCommand { get; set; }
        public IRelayCommand? ChangeItemCommand { get; set; }
        public IRelayCommand? AddItemCommand { get; set; }
        public IRelayCommand? RemoveItemCommand { get; set; }
        public IAsyncRelayCommand<User> SaveCommandAsync { get; set; }
        public IAsyncRelayCommand<User> DeleteCommandAsync { get; set; } 
        public IRelayCommand? DeleteItemCommand{ get; set; } 

        public UserViewModel(IPagerRepository<User> userRepo)
        {
            _userRepo = userRepo;
            SaveCommandAsync = new AsyncRelayCommand<User>(user => Save(user));
            DeleteCommandAsync = new AsyncRelayCommand<User>(user => Delete(user));
            Task.Run(LoadData);
        }
        protected override async Task LoadData()
        {
            JwtToken jwt = new JwtToken();
            if (jwt.Access_Token == null&&App.currentUser!=null)
            {
                jwt.Access_Token = App.currentUser.access_Token;
                jwt.Refresh_Token = App.currentUser.refresh_Token;
                _jwt = jwt;
            }
            var users = await _userRepo.GetAllAsync(page, ItemsPerPage, SearchKey, SortKey, ascending,_jwt);
            Users = new ObservableCollection<User>(users.Data); 
            TotalItems = users.TotalItems;
          }
        private async Task Save(User user)
        {
            bool exist = await _userRepo.ExistsByIdAsync(user.Id, _jwt);
            if (exist)
            {
                var jsonContent = await _userRepo.UpdateAsync(user, _jwt);
                Message(jsonContent);
            }
            else { MessageBox.Show("Ilyen felhasználó nem található az adatbázisban."); }
        }
        private async Task Delete(User user)
        {
           var jsonContent =await _userRepo.DeleteAsync(user.Id, _jwt);
            Message(jsonContent);
            Users.Remove(user);
        }
        public void Message(HttpResponseMessage message)
        {
            var response = message.Content.ReadAsStringAsync().Result;
            if (message.ReasonPhrase == "Bad Request")
            {
                MessageBox.Show($"{response}", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (message.ReasonPhrase == "OK")
            {
                MessageBox.Show($"{response}", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show($"{response}", "Bad Request", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
