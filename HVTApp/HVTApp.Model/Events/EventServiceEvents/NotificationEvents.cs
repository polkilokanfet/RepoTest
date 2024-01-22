using HVTApp.Model.Events.EventServiceEvents.Args;
using Prism.Events;

namespace HVTApp.Model.Events.EventServiceEvents
{
    public class PriceEngineeringTaskNotificationEvent : PubSubEvent<NotificationAboutPriceEngineeringTaskEventArg> { }
}