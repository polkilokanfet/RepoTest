using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
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

    public static class PriceEngineeringTaskStatusEnumExt
    {
        public static string ConvertToString(this PriceEngineeringTaskStatusEnum status)
        {
            switch (status)
            {
                case PriceEngineeringTaskStatusEnum.Created:
                    return "Менеджер создал задачу";
                case PriceEngineeringTaskStatusEnum.Started:
                    return "Менеджер запустил задачу на проработку";
                case PriceEngineeringTaskStatusEnum.Stopped:
                    return "Менеджер остановил проработку задачи";
                case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    return "Менеджер вернул задачу на доработку исполнителю";
                case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    return "Исполнитель отклонил задачу";
                case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    return "Исполнитель завершил работу над задачей";
                case PriceEngineeringTaskStatusEnum.Accepted:
                    return "Менеджер принял проработку задачи";
                case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                    return "Исполнитель направил задачу на проверку руководителю";
                case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                    return "Руководитель согласовал исполнителю проработку";
                case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                    return "Руководитель отклонил исполнителю проработку";
            }

            return status.ToString();
        }
    }

    public class PriceEngineeringTaskStatusMessage : IMessage
    {
        public DateTime Moment { get; }
        public string Message { get; }

        private PriceEngineeringTaskStatusMessage(DateTime moment, string message)
        {
            Moment = moment;
            Message = message;
        }

        public static PriceEngineeringTaskStatusMessage Convert(PriceEngineeringTaskStatus status)
        {
            var sb = new StringBuilder(status.StatusEnum.ConvertToString());

            if (string.IsNullOrWhiteSpace(status.Comment) == false)
            {
                sb.AppendLine(status.Comment);
            }

            return new PriceEngineeringTaskStatusMessage(status.Moment, sb.ToString());
        }
    }

}