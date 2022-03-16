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
        private RelayCommand _adminNavCommand;
        private RelayCommand _userNavCommand;

        public INavigationService NavigationService { get; set; }
        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        } 

        public MainViewModel(IMessenger messenger, INavigationService navigationService)
        {
            NavigationService = navigationService;

            messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });
        }
        
        public RelayCommand AdminNavCommand { get => _adminNavCommand ??= new RelayCommand(() =>
        {
            NavigationService.NavigateTo<AdminViewModel>();
        }); }        
        public RelayCommand UserNavCommand { get => _userNavCommand ??= new RelayCommand(() =>
        {
            NavigationService.NavigateTo<UserViewModel>();
        }); }

        
    }
}
