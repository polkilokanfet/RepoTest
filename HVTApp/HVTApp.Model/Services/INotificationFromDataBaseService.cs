using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface INotificationFromDataBaseService
    {
        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        /// <param name="unit"></param>
        void SaveNotificationInDataBase(NotificationUnit unit);

        void CheckMessagesInDbAndShowNotifications();

        void ShowNotification(NotificationUnit notificationUnit);
    }
}