using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events.EventServiceEvents
{
    public abstract class PriceEngeeniringTaskNotificationEventG<T> : PubSubEvent<T> where T : NotificationArgs<PriceEngineeringTask> { }

    public class PriceEngineeringTaskNotificationEvent : PriceEngeeniringTaskNotificationEventG<NotificationArgsPriceEngineeringTask> { }
}