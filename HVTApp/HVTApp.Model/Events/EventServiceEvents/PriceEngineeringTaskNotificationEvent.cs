using HVTApp.Model.Events.NotificationArgs;
using Prism.Events;

namespace HVTApp.Model.Events.EventServiceEvents
{
    public class PriceEngineeringTaskNotificationEvent : PubSubEvent<NotificationAboutPriceEngineeringTaskEventArg> { }
}