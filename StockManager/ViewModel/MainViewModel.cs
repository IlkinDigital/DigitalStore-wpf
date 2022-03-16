using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private RelayCommand _adminCommand;
        private RelayCommand _userCommand;

        private IStockManager StockManager;

        public MainViewModel(IStockManager stockManager)
        {
            StockManager = stockManager;
        }

        public List<Product> StockProducts { get; set; } = new();

        
    }
}
