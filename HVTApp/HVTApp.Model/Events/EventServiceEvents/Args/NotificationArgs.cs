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
        public IEnumerable<NotificationArgsItem> EventServiceItems { get; }

        protected NotificationArgs(T entity, IEnumerable<NotificationArgsItem> eventServiceItems)
        {
            Entity = entity;
            EventServiceItems = eventServiceItems;
        }
    }

    public class NotificationArgsPriceEngineeringTask : NotificationArgs<PriceEngineeringTask>
    {
        public NotificationArgsPriceEngineeringTask(PriceEngineeringTask entity, IEnumerable<NotificationArgsItem> eventServiceItems) 
            : base(entity, eventServiceItems)
        {
        }
    }

    public class NotificationArgsItem
    {
        public User User { get; }
        public Role Role { get; }
        public string Message { get; }

        public NotificationArgsItem(User user, Role role, string message)
        {
            User = user;
            Role = role;
            Message = message;
        }
    }
}