using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface INotificationFromDataBaseService
    {
        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        /// <param name="notification"></param>
        void SaveNotificationInDataBase(NotificationAboutPriceEngineeringTaskEventArg notification);

        void SaveNotificationInDataBase(EventServiceUnit unit);
    }
}