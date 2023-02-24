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
            services.AddTransient<IPagerRepository<Meal>, PagerRepository<Meal>>(x =>
            {
                return new PagerRepository<Meal>("api/Meal");
            });
            services.AddTransient<IGenericRepository<Mealtype>, GenericAPIRepository<Mealtype>>(x =>
            {
                return new PagerRepository<Mealtype>("api/MealType");
            });

            services.AddTransient<IPagerRepository<User>, PagerRepository<User>>(x =>
            {
                return new PagerRepository<User>("api/User");
            });
            // ViewModel, nézetmodel
            services.AddTransient<MealsViewModel>();
            services.AddTransient<UserViewModel>();
            services.AddTransient<LoginViewModel>();

            return services.BuildServiceProvider();
        }
    }
}

