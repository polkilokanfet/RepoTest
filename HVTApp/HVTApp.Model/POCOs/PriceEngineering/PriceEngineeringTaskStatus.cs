using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    public class PriceEngineeringTaskStatus : BaseEntity
    {
        [Designation("Id технико-стоимостной проработки"), Required, OrderStatus(900)]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Момент"), Required]
        public virtual DateTime Moment { get; set; } = DateTime.Now;

        [Designation("Комментарий"), MaxLength(1024)]
        public string Comment { get; set; }

        [Required]
        public PriceEngineeringTaskStatusEnum StatusEnum { get; set; }

        public override string ToString()
        {
            return $"{StatusEnum} {Moment}";
        }
    }

    public enum PriceEngineeringTaskStatusEnum
    {
        Created,
        Started,
        Stopped,
        RejectedByManager,
        RejectedByConstructor,
        FinishedByConstructor,
        Accepted,
        FinishedByConstructorGoToVerification,
        VerificationAcceptedByHead,
        VerificationRejectededByHead
    }
}