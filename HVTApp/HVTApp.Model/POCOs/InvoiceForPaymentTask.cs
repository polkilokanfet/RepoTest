using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Счёт на оплату (задание)")]
    public class InvoiceForPaymentTask : BaseEntity
    {
        [Designation("Задача ТСП")]
        public virtual PriceEngineeringTask PriceEngineeringTask { get; set; }
        
        [Designation("Задача ТСЕ")]
        public virtual TechnicalRequrements TechnicalRequrements { get; set; }

        public DateTime Moment { get; set; } = DateTime.Now;
    }
}