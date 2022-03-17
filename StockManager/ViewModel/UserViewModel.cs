using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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
    public class UserViewModel : ViewModelBase
    {
        private IMessenger Messenger;

        public float TotalCost { get; set; } = 0.0f;

        public UserViewModel(IMessenger messenger)
        {
            Messenger = messenger;
        }


    }
}
