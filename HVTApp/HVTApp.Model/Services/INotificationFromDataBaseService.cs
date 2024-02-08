using HVTApp.Model.Events.NotificationArgs;

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
    }
}