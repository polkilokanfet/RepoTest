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