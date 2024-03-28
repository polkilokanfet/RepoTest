using System;
using HVTApp.Model.POCOs;

namespace EmailNotificationsService
{
    public interface IEmailNotificationsService
    {
        void SendNotifications();
        event Action<NotificationUnit> SuccessSendNotificationEvent;
        event Action<NotificationUnit, Exception> NotSuccessSendNotificationEvent;
    }
}