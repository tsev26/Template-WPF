using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Template_WPF.Services;
using Template_WPF.Stores;
using Template_WPF.ViewModels;

namespace Template_WPF
{
    public partial class App : Application
    {
        private readonly IServiceProvider _servicesProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>();
            services.AddSingleton<MessageStore>();

            services.AddTransient<HomeViewModel>(CreateHomeViewModel);
            services.AddTransient<SecondViewModel>(CreateSecondViewModel);
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<INavigationService>(CreateHomeNavigationService);

            _servicesProvider = services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            INavigationService initialNavigationService = _servicesProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _servicesProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeViewModel>());
        }
        private INavigationService CreateSecondNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<SecondViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<SecondViewModel>());
        }
        private HomeViewModel CreateHomeViewModel(IServiceProvider serviceProvider)
        {
            return new HomeViewModel(
                CreateSecondNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<MessageStore>());
        }
        private SecondViewModel CreateSecondViewModel(IServiceProvider serviceProvider)
        {
            return new SecondViewModel(
                CreateHomeNavigationService(serviceProvider),
                serviceProvider.GetRequiredService<MessageStore>());
        }
    }
}
