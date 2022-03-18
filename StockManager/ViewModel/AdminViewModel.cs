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
    public class AdminViewModel : ViewModelBase
    {
        private IStockManager ProductsManager;
        private IMessenger Messenger;

        private string _namePrompt;
        public string NamePrompt
        {
            get => _namePrompt;
            set => Set(ref _namePrompt, value);
        }
        
        private string _descriptionPrompt;
        public string DescriptionPrompt
        {
            get => _descriptionPrompt;
            set => Set(ref _descriptionPrompt, value);
        }

        private string _pricePrompt;
        public string PricePrompt
        {
            get => _pricePrompt;
            set => Set(ref _pricePrompt, value);
        }

        public AdminViewModel(IMessenger messenger, IStockManager stockManager)
        {
            ProductsManager = stockManager;
            Messenger = messenger;
        }

        private void OutputError(string message) => MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(() =>
            {
                bool valid = true;
                float price = 0.0f;

                if (NamePrompt == "")
                {
                    OutputError("Enter the name of the product");
                    valid = false;
                }
                else if (DescriptionPrompt == "")
                {
                    OutputError("Enter the description of the product");
                    valid = false;
                }
                else if (PricePrompt == "")
                {
                    OutputError("Enter the price of the product");
                    valid = false;
                }

                try
                {
                    price = (float)Convert.ToDouble(PricePrompt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    valid = false;
                }

                if (valid)
                {
                    Product product = new()
                    {
                        Name = NamePrompt,
                        Description = DescriptionPrompt,
                        Price = price
                    };

                    var products = ProductsManager.Get();
                    if (products != null)
                    {
                        List<Product> productList = new(products);

                        Product? foundProduct = productList.Find(x => x.Name == product.Name &&
                                                                      x.Description == product.Description &&
                                                                      x.Price == product.Price);

                        if (foundProduct == null)
                        {
                            ProductsManager.Add(product);
                            Messenger?.Send<UpdateStockMessage>(new());

                            NamePrompt = "";
                            DescriptionPrompt = "";
                            PricePrompt = "";
                        }
                        else
                        {
                            OutputError("This product already exists");
                        }
                    }
                }
            });
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(() =>
            {
                if (MainViewModel.SelectedIndex != -1)
                {
                    ProductsManager.Remove(MainViewModel.SelectedIndex);
                    Messenger.Send<UpdateStockMessage>(new());
                }
                else
                {
                    OutputError("You didn't select a product");
                }
            });
        }
    }
}
