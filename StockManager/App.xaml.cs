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

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<AdminViewModel>();

            Container.Verify();
        }

        public void StartMain<Ty>() where Ty : ViewModelBase
        {
            Window window = new Window();
            var viewModel = Container.GetInstance<Ty>();

            window.DataContext = viewModel;
            
            window.ShowDialog();
        }
    }
}
