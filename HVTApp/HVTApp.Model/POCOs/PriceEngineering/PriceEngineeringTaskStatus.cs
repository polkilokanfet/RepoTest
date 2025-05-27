using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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

        [Designation("Комментарий")]
        public string Comment { get; set; }

        [Required]
        public int StatusEnum { get; set; }

        public override string ToString()
        {
            ScriptStep scriptStep = null;

            if (StatusEnum == ScriptStep.Start.Value)
                scriptStep = ScriptStep.Start;
            else if (StatusEnum == ScriptStep.Stop.Value)
                scriptStep = ScriptStep.Stop;
            else if (StatusEnum == ScriptStep.Create.Value)
                scriptStep = ScriptStep.Create;

            return scriptStep == null 
                ? $"{StatusEnum} {Moment}"
                : $"{scriptStep.Description} {StatusEnum} {Moment}";
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
            var sb = new StringBuilder(ScriptStep.FromValue(status.StatusEnum).Description);

            if (string.IsNullOrWhiteSpace(status.Comment) == false)
            {
                sb.AppendLine($"\n{status.Comment}");
            }

            return new PriceEngineeringTaskStatusMessage(status.Moment, sb.ToString());
        }
    }

}