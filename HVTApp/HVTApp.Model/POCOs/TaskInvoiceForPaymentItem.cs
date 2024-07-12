using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Счёт на оплату (строка задания)")]
    public class TaskInvoiceForPaymentItem : BaseEntity
    {
        public Guid TaskId { get; set; }

        [Designation("Задача ТСП")]
        public virtual PriceEngineeringTask PriceEngineeringTask { get; set; }

        [Designation("Задача ТСЕ")]
        public virtual TechnicalRequrements TechnicalRequrements { get; set; }

        [Designation("Связанное условие платежа"), Required]
        public virtual PaymentCondition PaymentCondition { get; set; }

        [NotMapped, NotForWrapper, NotForListView, NotForDetailsView]
        public IEnumerable<SalesUnit> SalesUnits => this.PriceEngineeringTask != null
            ? this.PriceEngineeringTask.SalesUnits
            : this.TechnicalRequrements.SalesUnits;
    }
}