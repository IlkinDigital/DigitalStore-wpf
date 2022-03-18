using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using StockManager.Messages;
using StockManager.Model;
using StockManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockManager.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private IMessenger Messenger;
        private IStockManager StockManager;

        public List<Product> Cart { get; set; } = new();

        private float _totalCost = 0.0f;
        public float TotalCost
        {
            get => _totalCost;
            set => Set(ref _totalCost, value);
        }

        public UserViewModel(IMessenger messenger, IStockManager stockManager)
        {
            Messenger = messenger;
            StockManager = stockManager;
        }

        private void OutputError(string message) => MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(() =>
            {
                if (MainViewModel.SelectedIndex != -1)
                {
                    try
                    {
                        var products = StockManager.Get();

                        var product = products[MainViewModel.SelectedIndex];
                        Cart.Add(product);
                        TotalCost += product.Price;
                    }
                    catch(Exception)
                    {
                        OutputError("There are no available products to purchase");
                    }
                }
                else
                    OutputError("You didn't select a product");
            });
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(() =>
            {
                if (MainViewModel.SelectedIndex != -1)
                {
                    var products = StockManager.Get();

                    try
                    {
                        Product selectedProduct = products[MainViewModel.SelectedIndex];
                        Product? foundProduct = Cart.Find((item) =>
                        {
                            return item.Name == selectedProduct.Name &&
                                    item.Description == selectedProduct.Description &&
                                    item.Price == selectedProduct.Price;
                        });

                        if (foundProduct != null)
                        {
                            Cart.Remove(foundProduct);
                            TotalCost -= foundProduct.Price;
                        }
                        else
                            OutputError("Selected product isn't in your cart");
                    }
                    catch(Exception)
                    {
                        OutputError("There are no available products to purchase");
                    }
                }
                else
                    OutputError("You didn't select a product");
            });
        }

        private RelayCommand _buyCommand;
        public RelayCommand BuyCommand
        {
            get => _buyCommand ??= new RelayCommand(() =>
            {
                Cart.Clear();
                TotalCost = 0.0f;
            });
        }
    }
}
