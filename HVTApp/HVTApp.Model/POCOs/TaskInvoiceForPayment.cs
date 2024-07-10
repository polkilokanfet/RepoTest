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
        [Designation("Строки счёта")]
        public virtual List<TaskInvoiceForPaymentItem> Items { get; set; } = new List<TaskInvoiceForPaymentItem>();

        [Designation("Старт задачи")]
        public DateTime? MomentStart { get; set; }

        [Designation("Финиш задачи")]
        public DateTime? MomentFinish { get; set; }

        [Designation("Исполнитель")]
        public virtual User BackManager { get; set; }

        [Designation("Требуется оригинал счёта")]
        public bool OriginalIsRequired { get; set; } = false;

        [Designation("Комментарий менеджера"), MaxLength(128)]
        public string Comment { get; set; }
    }
}