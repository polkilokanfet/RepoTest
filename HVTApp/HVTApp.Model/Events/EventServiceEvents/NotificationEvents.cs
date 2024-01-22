using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events.EventServiceEvents
{
    public abstract class PriceEngineeringTaskNotificationEventG<T> : PubSubEvent<T> where T : NotificationArgs<PriceEngineeringTask> { }

    public class PriceEngineeringTaskNotificationEvent : PriceEngineeringTaskNotificationEventG<NotificationArgsPriceEngineeringTask> { }

    /// <summary>
    /// Отправка по сети через приложение
    /// </summary>
    public interface ISendNotificationThroughApp
    {
        bool SendNotification(NotificationArgsPriceEngineeringTask args, NotificationItem item);
    }
}