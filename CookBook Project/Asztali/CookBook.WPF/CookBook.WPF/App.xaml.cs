using CookBook.API.Controllers.DTO;
using CookBook.ApiClient.Models;
using CookBook.ApiClient.Repositories;
using CookBook.Models.Models;
using CookBook.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace CookBook.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
   
        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();

        }
       
        public static ActualUser? currentUser { get; set;}
        public static bool IsRefreshing { get; set; }



        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            // Szolgáltatások
            services.AddTransient<IPagerRepository<MealDTO>, PagerRepository<MealDTO>>(x =>
            {
                return new PagerRepository<MealDTO>("api/Meal");
            });
            services.AddTransient<IGenericRepository<Mealtype>, GenericAPIRepository<Mealtype>>(x =>
            {
                return new PagerRepository<Mealtype>("api/MealType");
            });
            services.AddTransient<IGenericRepository<Measures>, GenericAPIRepository<Measures>>(x =>
            {
                return new PagerRepository<Measures>("api/Measure");
            });
            services.AddTransient<IPagerRepository<User>, PagerRepository<User>>(x =>
            {
                return new PagerRepository<User>("api/User");
            });
            services.AddTransient<IGenericRepository<User>, GenericAPIRepository<User>>(x =>
            {
                return new GenericAPIRepository<User>("api/Token");
            });
            services.AddTransient<IGenericRepository<Meal>, GenericAPIRepository<Meal>>(x =>
            {
                return new GenericAPIRepository<Meal>("api/Meal");
            });
            // ViewModel, nézetmodel
            services.AddTransient<MealsViewModel>();
            services.AddTransient<UserViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();
            return services.BuildServiceProvider();
        }
    }
}

