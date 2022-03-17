using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using StockManager.Messages;
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

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(() =>
            {
                bool valid = true;
                float price = 0.0f;

                if (NamePrompt == null || NamePrompt == "")
                {
                    MessageBox.Show("Forgot to enter name of the product", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    valid = false;
                }
                else if (DescriptionPrompt == null || DescriptionPrompt == "")
                {
                    MessageBox.Show("Forgot to enter description of the product", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    valid = false;
                }
                else if (PricePrompt == null || PricePrompt == "")
                {
                    MessageBox.Show("Forgot to enter price of the product", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    ProductsManager.Add(new()
                    {
                        Name = NamePrompt,
                        Description = DescriptionPrompt,
                        Price = price
                    });

                    Messenger?.Send<UpdateStockMessage>(new());
                }
            });
        }

        public string? NamePrompt { get; set; }
        public string? DescriptionPrompt { get; set; }
        public string? PricePrompt { get; set; }

        public AdminViewModel(IMessenger messenger, IStockManager stockManager)
        {
            ProductsManager = stockManager;
            Messenger = messenger;
        }

    }
}
