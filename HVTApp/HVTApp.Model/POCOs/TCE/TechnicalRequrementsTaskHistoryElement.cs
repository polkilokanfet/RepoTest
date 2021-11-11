using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Статус тех.задания (задача)")]
    public class TechnicalRequrementsTaskHistoryElement : BaseEntity
    {
        public virtual Guid TechnicalRequrementsTaskId { get; set; }

        [Designation("Момент"), Required, OrderStatus(90)]
        public DateTime Moment { get; set; } = DateTime.Now;

        [Designation("Тип"), Required, OrderStatus(80)]
        public TechnicalRequrementsTaskHistoryElementType Type { get; set; } = TechnicalRequrementsTaskHistoryElementType.Create;

        [Designation("Комментарий"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; } = string.Empty;
    }

    public enum TechnicalRequrementsTaskHistoryElementType
    {
        /// <summary>
        /// создано
        /// </summary>
        Create,

        /// <summary>
        /// стартовано
        /// </summary>
        Start,

        //завершено
        Finish,

        /// <summary>
        /// Отклонено БэкМенеджером
        /// </summary>
        Reject,

        /// <summary>
        /// Отклонено ФронтМенеджером
        /// </summary>
        RejectByFrontManager,

        /// <summary>
        /// Остановлено ФМ
        /// </summary>
        Stop,

        /// <summary>
        /// Поручено
        /// </summary>
        Instruct,

        /// <summary>
        /// Принято
        /// </summary>
        Accept
    }
}