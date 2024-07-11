using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Счёт на оплату (задание)")]
    public class TaskInvoiceForPayment : BaseEntity
    {
        [Designation("Строки счёта"), Required, NotForListView]
        public virtual List<TaskInvoiceForPaymentItem> Items { get; set; } = new List<TaskInvoiceForPaymentItem>();

        [Designation("Старт задачи"), OrderStatus(200)]
        public DateTime? MomentStart { get; set; }

        [Designation("Финиш задачи"), OrderStatus(190)]
        public DateTime? MomentFinish { get; set; }

        [Designation("Исполнитель"), OrderStatus(50)]
        public virtual User BackManager { get; set; }

        [Designation("Требуется оригинал счёта"), NotForListView]
        public bool OriginalIsRequired { get; set; } = false;

        [Designation("Комментарий менеджера"), MaxLength(128)]
        public string Comment { get; set; }
    }
}