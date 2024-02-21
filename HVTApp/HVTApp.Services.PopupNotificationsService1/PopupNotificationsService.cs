using System;
using System.Windows;
using HVTApp.Infrastructure.Interfaces.Services;
using Prism.Regions;

namespace HVTApp.Services.PopupNotificationsService1
{
    public class PopupNotificationsService : IPopupNotificationsService
    {
        private readonly IRegionManager _regionManager;

        public PopupNotificationsService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void ShowNotification(string text, string title = null, Action action = null)
        {
            //переводим всплывающее окно в основной поток
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    new PopupWindow2(text, title, action).Show();
                });
        }
    }
}