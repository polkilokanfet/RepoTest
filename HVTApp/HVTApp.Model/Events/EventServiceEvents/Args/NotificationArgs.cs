using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.EventServiceEvents.Args
{
    /// <summary>
    /// Аргументы для EventService
    /// </summary>
    public abstract class NotificationArgs<T> 
        where T : BaseEntity
    {
        public T Entity { get; }
        public IEnumerable<NotificationItem> EventServiceItems { get; }

        protected NotificationArgs(T entity, IEnumerable<NotificationItem> eventServiceItems)
        {
            Entity = entity;
            EventServiceItems = eventServiceItems;
        }
    }

    public class NotificationArgsPriceEngineeringTask : NotificationArgs<PriceEngineeringTask>
    {
        public NotificationArgsPriceEngineeringTask(PriceEngineeringTask entity, IEnumerable<NotificationItem> eventServiceItems) 
            : base(entity, eventServiceItems)
        {
        }
    }

    public class NotificationItem
    {
        public User User { get; }
        public Role Role { get; }
        public string Message { get; }

        public NotificationItem(User user, Role role, string message)
        {
            User = user;
            Role = role;
            Message = message;
        }
    }
}