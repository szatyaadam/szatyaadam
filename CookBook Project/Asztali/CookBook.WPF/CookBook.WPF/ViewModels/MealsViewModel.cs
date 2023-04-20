using CookBook.WPF.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using CommunityToolkit.Mvvm.Input;
using CookBook.ApiClient.Models;
using CookBook.API.Controllers.DTO;
using System.Net.Http;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.WPF.ViewModels
{
    public class MealsViewModel : PagerViewModel, IEditCommand<MealDTO>
    {
        private JwtToken _jwt;
        public JwtToken Jwt
        {
            get { return _jwt; }
            set {SetProperty(ref _jwt, value); }
        }
        private readonly IPagerRepository<MealDTO> _mealRepo;
        private readonly IGenericRepository<Meal> _mealRepos;
        private IGenericRepository<Mealtype> _MealtypeRepo;
        private IGenericRepository<Measures> _MeasureRepo;
        private ObservableCollection<MealDTO> _meals = new();
        public ObservableCollection<MealDTO?> Meals
        {
            get { return _meals; }
            set{ SetProperty(ref _meals, value);}
        }
        private ObservableCollection<Measures> _measure = new();
        public ObservableCollection<Measures> Measure
        {
            get { return _measure; }
            set { SetProperty(ref _measure, value); }
        }
        private ObservableCollection<Mealtype> _mealType = new();
        public ObservableCollection<Mealtype> mealType
        {
            get { return _mealType; }
            set { SetProperty(ref _mealType, value); }
        }
        private MealDTO? _selectedMeal = new();
        public MealDTO? SelectedMeal
        {
            get { return _selectedMeal; }
            set
            {
                SetProperty(ref _selectedMeal, value);
                DeleteCommandAsync.NotifyCanExecuteChanged(); 
            }
        }
        private Mealtype _newMealType = new();
        public Mealtype NewMealType
        {
            get { return _newMealType; }
            set{ SetProperty(ref _newMealType, value); }
        }
        private Mealtype _changeMealType = new();
        public Mealtype ChangeMealType
        {
            get { return _changeMealType; }
            set { SetProperty(ref _changeMealType, value); }
        }
        private Measures _newMeasure = new();
        public Measures NewMeasure
        {
            get { return _newMeasure; }
            set{ SetProperty(ref _newMeasure, value);  }
        }
        private Measures _selectedMeasure = new();
        public Measures SelectedMeasure
        {
            get { return _selectedMeasure; }
            set { SetProperty(ref _selectedMeasure, value); }
        }
        private Measures _changeMeasure = new();
        public Measures ChangeMeasure
        {
            get { return _changeMeasure; }
            set  { SetProperty(ref _changeMeasure, value); }
        }
        private MaterialDTO _newMaterial = new();
        public MaterialDTO NewMaterial
        {
            get { return _newMaterial; }
            set { SetProperty(ref _newMaterial, value);}
        }
        private IngredientDTO _newIngredient = new();
        public IngredientDTO NewIngredient
        {
            get { return _newIngredient; }
            set { SetProperty(ref _newIngredient, value);}
        }
        private List<IngredientDTO> _newIngredients = new();
        public List<IngredientDTO> NewIngredients
        {
            get { return _newIngredients; }
            set { SetProperty(ref _newIngredients, value); }
        }
        private List<IngredientDTO?> _deleteIngredients = new();
        public List<IngredientDTO?> DeleteIngredients
        {
            get { return _deleteIngredients; }
            set { SetProperty(ref _deleteIngredients, value); }
        }
        public IRelayCommand? NewCommand { get; set; }
        public IRelayCommand? NewItemCommand { get; set; }
        public IRelayCommand? ChangeItemCommand { get; set; }
        public IRelayCommand? AddItemCommand { get; set; }
        public IRelayCommand? RemoveItemCommand { get; set; }
        public IAsyncRelayCommand<MealDTO> SaveCommandAsync { get; set; }
        public IAsyncRelayCommand<MealDTO> DeleteCommandAsync { get; set; }
        public IRelayCommand? DeleteItemCommand { get; set; }
        public MealsViewModel(IPagerRepository<MealDTO> mealRepo, IGenericRepository<Mealtype> mealtypeRepo, IGenericRepository<Measures> measureRepo, IGenericRepository<Meal> mealRepos)
        {
            _mealRepos = mealRepos;
            _mealRepo = mealRepo;
            _MealtypeRepo = mealtypeRepo;
            _MeasureRepo = measureRepo;
            NewCommand = new RelayCommand(New);
            NewItemCommand = new RelayCommand(NewItem);
            ChangeItemCommand = new RelayCommand(ChangeItem);
            AddItemCommand = new RelayCommand(AddItem);
            RemoveItemCommand = new RelayCommand<IngredientDTO>(ingredient => RemoveItem(ingredient));
            SaveCommandAsync = new AsyncRelayCommand<MealDTO>(meal => Save(meal));
            DeleteCommandAsync = new AsyncRelayCommand<MealDTO>(meal => Delete(meal));
            DeleteItemCommand = new RelayCommand(DeleteItem);
            Task.Run(LoadData);
        }
        protected override async Task LoadData()
        {
            JwtToken jwt = new JwtToken();
            if (jwt.Access_Token == null && App.currentUser != null)
            {
                jwt.Access_Token = App.currentUser.access_Token;
                jwt.Refresh_Token = App.currentUser.refresh_Token;
                _jwt = jwt;
            }
            var meals = await _mealRepo.GetAllAsync(page, ItemsPerPage, SearchKey, SortKey, ascending, _jwt);
            Meals = new ObservableCollection<MealDTO>(meals.Data);
            var mealtype = await _MealtypeRepo.GetAllAsync();
            mealType = new ObservableCollection<Mealtype>(mealtype);
            var measure = await _MeasureRepo.GetAllAsync();
            Measure = new ObservableCollection<Measures>(measure);
            TotalItems = meals.TotalItems;
        }
        private async void RemoveItem(IngredientDTO ingredient)
        {
            try
            {
                DeleteIngredients.Add(ingredient);
                SelectedMeal.Ingredients.Remove(ingredient);
                MessageBox.Show($"A {ingredient.Materials.IngredientName} hozzávalót\n töröltük, töröljönmásikat  vagy nyomjon rá a mentésre.", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show($"Hibás adat", "Bad Request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddItem()
        {
            try
            {
                Measures newitems = new Measures
                {
                    Id = SelectedMeasure.Id,
                    Measure = SelectedMeasure.Measure
                };
                MaterialDTO newitem = new MaterialDTO
                {
                    Id = NewMaterial.Id,
                    UnitOfMeasure = newitems,
                    IngredientName = NewMaterial.IngredientName
                };
                IngredientDTO newadd = new IngredientDTO
                {
                    Id = NewIngredient.Id,
                    Quantity = NewIngredient.Quantity,
                    Materials = newitem
                };
                NewIngredients.Add(newadd);
                MessageBox.Show($"Hozzáadtuk a hozzávalót\n adjon hozzá újjat vagy nyomjon rá a mentésre.", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch 
            {
                MessageBox.Show($"Valami hiba történt ", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void ChangeItem()
        {
            try
            {
                if (ChangeMealType.MealTypeName != null)
                {
                    var jsonContent = await _MealtypeRepo.ChangeItemAsync(ChangeMealType, _jwt);
                    var mealtype = await _MealtypeRepo.GetAllAsync();
                    mealType = new ObservableCollection<Mealtype>(mealtype);
                    Message(jsonContent);
                }
                else if (ChangeMeasure.Id != 0 && ChangeMeasure != null)
                {
                    var jsonContent = await _MeasureRepo.ChangeItemAsync(ChangeMeasure, _jwt);
                    var measure = await _MeasureRepo.GetAllAsync();
                    Measure = new ObservableCollection<Measures>(measure);
                    Message(jsonContent);
                }
            }
            catch 
            {
                MessageBox.Show($"Valami hiba történt ", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void NewItem()
        {
            try
            {
                if (NewIngredient.Quantity != null)
                {
                    SelectedMeal.Ingredients = _newIngredients;
                    var jsonContent = await _mealRepo.UpdateAsync(SelectedMeal, _jwt);
                }
                if (NewMealType.MealTypeName != null)
                {
                    var jsonContent = await _MealtypeRepo.NewItemAsync(NewMealType, _jwt);
                    var mealtype = await _MealtypeRepo.GetAllAsync();
                    mealType = new ObservableCollection<Mealtype>(mealtype);
                    Message(jsonContent);
                }
                else if (NewMeasure.Measure != String.Empty)
                {
                    var jsonContent = await _MeasureRepo.NewItemAsync(NewMeasure, _jwt);
                    NewMeasure.Measure = string.Empty;
                    var measure = await _MeasureRepo.GetAllAsync();
                    Measure = new ObservableCollection<Measures>(measure);
                    Message(jsonContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba {ex}", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void New()
        {
            SelectedMeal = new MealDTO();
        }

        private async Task Save(MealDTO? meal)
        {
            bool exist = await _mealRepo.ExistsByIdAsync(meal.Id);
            if (exist)
            {
                List<Ingredient?> newingredients = new List<Ingredient>();
                for (int i = 0; i < _newIngredients.Count; i++)
                {
                    if (_newIngredients.ElementAt(i).Quantity != null || _newIngredients.ElementAt(i).Materials.IngredientName != null)
                    {
                        Measures newitems = new Measures
                        {
                            Id = _newIngredients.ElementAt(i).Materials.UnitOfMeasure.Id,
                            Measure = _newIngredients.ElementAt(i).Materials.UnitOfMeasure.Measure
                        };
                        Material newitem = new Material
                        {
                            Id = _newIngredients.ElementAt(i).Materials.Id,
                            UnitOfMeasure = newitems,
                            IngredientName = _newIngredients.ElementAt(i).Materials.IngredientName
                        };
                        Ingredient newadd = new Ingredient
                        {
                            Id = _newIngredients.ElementAt(i).Id,
                            Quantity = _newIngredients.ElementAt(i).Quantity,
                            Materials = newitem
                        };
                        newingredients.Add(newadd);
                    }
                }
                for (int i = 0; i < meal.Ingredients.Count; i++)
                {
                    Measures newitems = new Measures
                    {
                        Id = meal.Ingredients.ElementAt(i).Materials.UnitOfMeasure.Id,
                        Measure = meal.Ingredients.ElementAt(i).Materials.UnitOfMeasure.Measure
                    };
                    Material newitem = new Material
                    {
                        Id = meal.Ingredients.ElementAt(i).Materials.Id,
                        UnitOfMeasure = newitems,
                        IngredientName = meal.Ingredients.ElementAt(i).Materials.IngredientName
                    };
                    Ingredient newadd = new Ingredient
                    {
                        Id = meal.Ingredients.ElementAt(i).Id,
                        Quantity = meal.Ingredients.ElementAt(i).Quantity,
                        Materials = newitem
                    };
                    newingredients.Add(newadd);
                }
                Meal newMeal = new Meal
                {
                    Id = meal.Id,
                    MealName = meal.MealName,
                    PreperationTime = meal.PreperationTime,
                    Price = meal.Price,
                    Discription = meal.Discription,
                    Ingredients = newingredients,
                    MealType = meal.Mealtype,
                    UserId = meal.UserId
                };

                for (int i = 0; i < newingredients.Count; i++)
                {
                    for (int x = 0; x < DeleteIngredients.Count(); x++)
                    {
                        if (DeleteIngredients.ElementAt(x).Materials.Id == newingredients.ElementAt(i).Materials.Id)
                        {
                            newingredients.Remove(newingredients.ElementAt(i));
                            DeleteIngredients.Remove(DeleteIngredients.ElementAt(x));
                        }
                    }
                }
                var jsonContent = await _mealRepos.UpdateAsync(newMeal, _jwt);
                Message(jsonContent);
                var meals = await _mealRepo.GetAllAsync(page, ItemsPerPage, SearchKey, SortKey, ascending, _jwt);
                Meals = new ObservableCollection<MealDTO>(meals.Data);
            }

            else
            {
                meal.Ingredients = _newIngredients;
                var jsonContent = await _mealRepo.InsertAsync(meal, _jwt);
                Meals.Insert(0, meal);
                Message(jsonContent);
            }
        }
        private async Task Delete(MealDTO meal)
        {
            var jsonContent = await _mealRepo.DeleteAsync(meal.Id, _jwt);
            Message(jsonContent);
            Meals.Remove(meal);
        }
        private async void DeleteItem()
        {
            try
            {
                if (ChangeMealType.Id != 0 && ChangeMealType != null)
                {
                    var jsonContent = await _MealtypeRepo.DeleteAsync(ChangeMealType.Id, _jwt);
                    var mealtype = await _MealtypeRepo.GetAllAsync();
                    mealType = new ObservableCollection<Mealtype>(mealtype);
                    mealtype.Remove(NewMealType);
                    Message(jsonContent);
                }
                else if (ChangeMeasure.Id != 0 && ChangeMeasure != null)
                {
                    var jsonContent = await _MeasureRepo.DeleteAsync(ChangeMeasure.Id, _jwt);
                    var measure = await _MeasureRepo.GetAllAsync();
                    Measure = new ObservableCollection<Measures>(measure);

                    Message(jsonContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            else MessageBox.Show($"{response}", "Message", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}

