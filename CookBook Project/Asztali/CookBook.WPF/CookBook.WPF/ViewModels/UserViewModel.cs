using CookBook.ApiClient.Repositories;
using CommunityToolkit.Mvvm.Input;
using CookBook.WPF.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CookBook.Models.Models;
using CookBook.WPF.ViewModels;

namespace CookBook.WPF.ViewModels
{
    public class UserViewModel : PagerViewModel , IEditCommand<User>
    {
        private IPagerRepository<User> _userRepo;
      


        private ObservableCollection<User> _users = new();
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                SetProperty(ref _users, value);
            }

        }


        private User  _selectedUser = new();
        public User SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }

        public IRelayCommand NewCommand { get; set; }
        public IAsyncRelayCommand<User> SaveCommandAsync { get; set; }
        public IAsyncRelayCommand<User> DeleteCommandAsync { get; set; }

        public UserViewModel(IPagerRepository<User> userRepo)
        {
            _userRepo = userRepo;
            NewCommand = new RelayCommand(New);
            SaveCommandAsync = new AsyncRelayCommand<User>(user => Save(user));
            DeleteCommandAsync = new AsyncRelayCommand<User>(user => Delete(user));
            Task.Run(LoadData);
        }
        //amig nem történik meg az autentikáció nem fog megjeleníteni semmit 
        protected override async Task LoadData()
        {
            var users = await _userRepo.GetAllAsync(page, ItemsPerPage, SearchKey, SortKey, ascending);
            Users = new ObservableCollection<User>(users.Data); 
            TotalItems = users.TotalItems;

        }
        private void New()
        {
            SelectedUser = new User();
        }

        private async Task Save(User user)
        {
            bool exist = await _userRepo.ExistsByIdAsync(user.Id);
      
            if (exist)
            {
                await _userRepo.UpdateAsync(user.Id, user);
            }
         
            else
            {
               
                await _userRepo.InsertAsync(user);
          
                Users.Insert(0, user);
            }
        }

        private async Task Delete(User user)
        {
            await _userRepo.DeleteAsync(user.Id);
            Users.Remove(user);
        }
    }

}
