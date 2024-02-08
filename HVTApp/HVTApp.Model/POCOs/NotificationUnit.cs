using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class NotificationUnit : BaseEntity
    {
        public EventServiceActionType ActionType { get; set; }

        public Guid TargetEntityId { get; set; }

        #region Sender

        /// <summary>
        /// отправитель
        /// </summary>
        [Required]
        public virtual User SenderUser { get; set; }

        public Guid SenderUserId { get; set; } = GlobalAppProperties.User.Id;

        /// <summary>
        /// роль отправителя
        /// </summary>
        public Role SenderRole { get; set; } = GlobalAppProperties.User.RoleCurrent;

        #endregion

        #region Recipient

        /// <summary>
        /// Получатель
        /// </summary>
        [Required]
        public virtual User RecipientUser { get; set; }

        public Guid RecipientUserId { get; set; }

        /// <summary>
        /// Роль получателя
        /// </summary>
        public Role RecipientRole { get; set; }

        #endregion
    }
}