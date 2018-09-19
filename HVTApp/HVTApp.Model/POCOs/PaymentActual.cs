using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Платеж совершенный")]
    public partial class PaymentActual : BaseEntity
    {
        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Сумма")]
        public double Sum { get; set; }

        [Designation("Комментарий")]
        public string Comment { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()}: {Sum}";
        }
    }
}