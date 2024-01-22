using HVTApp.Model.Events.EventServiceEvents.Args;

namespace HVTApp.Model.Events.EventServiceEvents
{
    /// <summary>
    /// Отправка по сети через приложение
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        bool SendNotification(NotificationAboutPriceEngineeringTaskEventArg item);
    }
}