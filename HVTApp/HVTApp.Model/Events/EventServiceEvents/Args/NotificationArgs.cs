using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Events.EventServiceEvents.Args
{
    /// <summary>
    /// Аргументы для EventService
    /// </summary>
    public abstract class NotificationArgs<T> 
        where T : BaseEntity
    {
        public T Entity { get; }
        public IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> EventServiceItems { get; }

        protected NotificationArgs(T entity, IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> eventServiceItems)
        {
            Entity = entity;
            EventServiceItems = eventServiceItems;
        }
    }
}