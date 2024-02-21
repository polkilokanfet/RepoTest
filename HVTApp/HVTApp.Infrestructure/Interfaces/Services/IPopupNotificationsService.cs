using System;

namespace HVTApp.Infrastructure.Interfaces.Services
{
    public interface IPopupNotificationsService
    {
        void ShowNotification(string text, string title = null, Action action = null);
    }
}