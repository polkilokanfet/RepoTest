using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.NotificationArgs
{
    public class NotificationUnit : BaseEntity
    {
        public EventServiceActionType ActionType { get; set; }

        public Guid TargetEntityId { get; set; }

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
        public User RecipientUser { get; set; }

        /// <summary>
        /// ���� ����������
        /// </summary>
        public Role RecipientRole { get; set; }

        #endregion
    }
}