using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using StockManager.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Services.Classes
{
    public class NavigationService
    {
        private readonly IMessenger _messenger;

        public NavigationService(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void NavigateTo<Ty>() where Ty : ViewModelBase
        {
            _messenger.Send(new NavigationMessage { ViewModelType = typeof(Ty) });
        }
    }
}
