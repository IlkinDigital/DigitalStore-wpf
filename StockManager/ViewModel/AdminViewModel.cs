using GalaSoft.MvvmLight;
using StockManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        private IStockManager ProductsManager;

        public string? NamePrompt { get; set; }
        public string? DescriptionPrompt { get; set; }
        public string? PricePropmpt { get; set; }

        public AdminViewModel(IStockManager stockManager)
        {
            ProductsManager = stockManager;
        }

    }
}
