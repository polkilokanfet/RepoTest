using HVTApp.Model.Events.NotificationArgs;

namespace HVTApp.Model.Events.EventServiceEvents
{
    /// <summary>
    /// Отправка по сети через приложение
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        bool SendNotification(NotificationUnit unit);
    }
}