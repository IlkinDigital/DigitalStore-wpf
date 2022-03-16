using StockManager.ViewModel;
using System;
using System.Collections.Generic;
using SimpleInjector; 
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using StockManager.Services.Interfaces;
using StockManager.Services.Classes;
using GalaSoft.MvvmLight.Messaging;
using StockManager.View;
using StockManager.Services;

namespace StockManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            StartMain<MainViewModel>();

            base.OnStartup(e);
        }

        public void Register()
        {
            Container = new Container();

            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IStockManager, JsonStockManager>();

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<UserViewModel>();
            Container.RegisterSingleton<AdminViewModel>();

            Container.Verify();
        }

        public void StartMain<Ty>() where Ty : ViewModelBase
        {
            Window window = new MainView();
            var viewModel = Container?.GetInstance<Ty>();

            window.DataContext = viewModel;
            
            window.ShowDialog();
        }
    }
}
