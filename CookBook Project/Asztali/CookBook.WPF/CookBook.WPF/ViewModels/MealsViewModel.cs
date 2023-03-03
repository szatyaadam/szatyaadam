using CookBook.WPF.Commands;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;


namespace CookBook.WPF.ViewModels
{
    public class MealsViewModel : PagerViewModel, IEditCommand<Meal> 
    {
        private readonly IPagerRepository<Meal> _mealRepo;
        private IGenericRepository<Mealtype> _MealtypeRepo;

        private ObservableCollection<Meal> _meals = new();
        public ObservableCollection<Meal> Meals
        {
            get { return _meals; }
            set
            {
                SetProperty(ref _meals, value);

            }
        }


        private ObservableCollection<Mealtype> _mealType = new();
        public ObservableCollection<Mealtype> mealType
        {
            get { return _mealType; }
            set { SetProperty(ref _mealType, value); }
        }

        private Meal _selectedMeal = new();
        public Meal SelectedMeal
        {
            get { return _selectedMeal; }
            set
            {
                SetProperty(ref _selectedMeal, value);
                DeleteCommandAsync.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand NewCommand { get; set; }
        public IAsyncRelayCommand<Meal> SaveCommandAsync { get; set; }
        public IAsyncRelayCommand<Meal> DeleteCommandAsync { get; set; }

        public MealsViewModel(IPagerRepository<Meal> mealRepo, IGenericRepository<Mealtype> mealtypeRepo)
        {
            _mealRepo = mealRepo;
            _MealtypeRepo = mealtypeRepo;
            NewCommand = new RelayCommand(New);
            SaveCommandAsync = new AsyncRelayCommand<Meal>(meal => Save(meal));
            DeleteCommandAsync = new AsyncRelayCommand<Meal>(meal => Delete(meal), CanDelete);     
            Task.Run(LoadData);
        }

        protected override async Task LoadData()
        {
            
            var meals = await _mealRepo.GetAllAsync(page, ItemsPerPage, SearchKey, SortKey, ascending);
            Meals = new ObservableCollection<Meal>(meals.Data);
            var mealtype = await _MealtypeRepo.GetAllAsync();
            mealType = new ObservableCollection<Mealtype>(mealtype);
            TotalItems = meals.TotalItems;
        }
        private void New()
        {
            SelectedMeal = new Meal();
        }

        private async Task Save(Meal meal)
        {
            bool exist = await _mealRepo.ExistsByIdAsync(meal.Id);
   
            if (exist)
            {
                await _mealRepo.UpdateAsync(meal.Id, meal);
            }

            else
            {
 
                await _mealRepo.InsertAsync(meal);

                Meals.Insert(0, meal);
            }
        }

        private bool CanDelete(Meal meal)
        {

            if (meal != null)
            {
                return meal.Id > 0;
            }
            return false;
        }
        private async Task Delete(Meal meal)
        {
            await _mealRepo.DeleteAsync(meal.Id);
            Meals.Remove(meal);
        }
    }

}
