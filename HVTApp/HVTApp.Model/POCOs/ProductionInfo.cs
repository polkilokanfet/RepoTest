using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    public partial class ProductionInfo : BaseEntity
    {
        [Designation("Id ТСП (для производства)")]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Заказ")]
        public virtual Order Order { get; set; }

        [Designation("Позиция"), MaxLength(4)]
        public string OrderPosition { get; set; }

        [Designation("Номер"), MaxLength(10)]
        public string SerialNumber { get; set; }

        [Designation("Срок сборки")]
        public int? AssembleTerm { get; set; }

        [Designation("Сигнал менеджера о производстве")]
        public DateTime SignalToStartProduction { get; set; } = DateTime.Now;

        [Designation("Дата размещения в производстве")]
        public DateTime? SignalToStartProductionDone { get; set; }

        [Designation("Дата начала производства")]
        public DateTime? StartProductionDate { get; set; }

        [Designation("Дата комплектации")]
        public DateTime? PickingDate { get; set; }

        [Designation("Плановая дата окончания производства")]
        public DateTime? EndProductionPlanDate { get; set; }

        [Designation("Дата окончания производства")]
        public DateTime? EndProductionDate { get; set; }


        public override string ToString()
        {
            return this.Order != null 
                ? $"з/з {this.Order.Number}, сер.№{this.SerialNumber}" 
                : "Заказ не открыт";
        }
    }
}