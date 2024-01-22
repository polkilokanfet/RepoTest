using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.EventServiceEvents.Args
{
    /// <summary>
    /// ��������� ��� EventService
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

    public class NotificationArgsPriceEngineeringTask : NotificationArgs<PriceEngineeringTask>
    {
        public NotificationArgsPriceEngineeringTask(PriceEngineeringTask entity, IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> eventServiceItems) 
            : base(entity, eventServiceItems)
        {
        }
    }

    /// <summary>
    /// ������� �����������
    /// </summary>
    public class NotificationAboutPriceEngineeringTaskEventArg
    {
        /// <summary>
        /// ��������� � ������������ ������ ���
        /// </summary>
        public PriceEngineeringTask PriceEngineeringTask { get; }

        #region Sender

        /// <summary>
        /// �����������
        /// </summary>
        public User SenderUser { get; set; } = GlobalAppProperties.User;

        /// <summary>
        /// ���� �����������
        /// </summary>
        public Role SenderRole { get; set; } = GlobalAppProperties.User.RoleCurrent;

        #endregion

        #region Recipient

        /// <summary>
        /// ����������
        /// </summary>
        public User RecipientUser { get; }

        /// <summary>
        /// ���� ����������
        /// </summary>
        public Role RecipientRole { get; }

        #endregion

        public string Message { get; }

        public NotificationAboutPriceEngineeringTaskEventArg(PriceEngineeringTask priceEngineeringTask, User recipientUser, Role recipientRole, string message)
        {
            PriceEngineeringTask = priceEngineeringTask;
            RecipientUser = recipientUser;
            RecipientRole = recipientRole;
            Message = message;
        }
    }
}