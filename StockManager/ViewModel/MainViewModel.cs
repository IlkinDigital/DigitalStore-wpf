using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using StockManager.Services.Interfaces;
using StockManager.Messages;
using StockManager.Model;
using StockManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        } 

        public INavigationService NavigationService { get; set; }

        private RelayCommand _adminNavCommand;
        public RelayCommand AdminNavCommand { get => _adminNavCommand ??= new RelayCommand(() =>
        {
            NavigationService.NavigateTo<AdminViewModel>();
        }); }        

        private RelayCommand _userNavCommand;
        public RelayCommand UserNavCommand { get => _userNavCommand ??= new RelayCommand(() =>
        {
            NavigationService.NavigateTo<UserViewModel>();
        }); }

        private readonly IStockManager StockManager;
        private readonly IMessenger Messenger;

        private List<Product> _producList = new();
        public List<Product> ProductList { get => _producList; set => _producList = value; }


        public MainViewModel(IMessenger messenger, INavigationService navigationService, IStockManager stockManager)
        {
            Messenger = messenger;
            NavigationService = navigationService;
            StockManager = stockManager;

            UpdateProductList();

            // Stock update message registration
            Messenger.Register<UpdateStockMessage>(this, message => UpdateProductList());

            // Navigation message registration
            Messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });
        }
        
        private void UpdateProductList()
        {
            var products = StockManager.Get();
            if (products != null)
                ProductList = new(products);
        }
    }
}
