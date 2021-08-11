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
        public TechnicalRequrementsTaskHistoryElementType Type { get; set; }

        [Designation("Комментарий"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }
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
        /// Отклонено БМ
        /// </summary>
        Reject,

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