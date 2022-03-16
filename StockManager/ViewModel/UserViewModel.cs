using GalaSoft.MvvmLight;
using StockManager.Model;
using StockManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private IStockManager StockManager;

        private List<Product> _producList = new();
        public List<Product> ProductList { get => _producList; set => _producList = value; }    

        public UserViewModel(IStockManager stockManager)
        {
            StockManager = stockManager;
            UpdateProductList();
        }

        private void UpdateProductList()
        {
            ProductList = new(StockManager.Get());
        }
    }
}
