using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        [Designation("Финиш (экономиста)"), OrderStatus(190)]
        public DateTime? MomentFinish { get; set; }

        [Designation("Финиш (плановика)"), OrderStatus(190)]
        public DateTime? MomentFinishByPlanMaker { get; set; }

        [Designation("Экономист"), OrderStatus(50)]
        public virtual User BackManager { get; set; }

        [Designation("Плановик"), OrderStatus(51)]
        public virtual User PlanMaker { get; set; }

        [Designation("Требуется оригинал счёта"), NotForListView]
        public bool OriginalIsRequired { get; set; } = false;

        [Designation("Комментарий менеджера"), MaxLength(128)]
        public string Comment { get; set; }

        [NotMapped]
        public bool PlanMakerIsRequired
        {
            get
            {
                var salesUnits = Items.SelectMany(item => item.SalesUnits).ToList();
                var orders = salesUnits.Select(salesUnit => salesUnit.Order).ToList();

                if (orders.Any(order => order == null))
                    return true;

                if (orders.Any(order => string.IsNullOrWhiteSpace(order.Number)))
                    return true;

                if (salesUnits.Any(salesUnit => string.IsNullOrWhiteSpace(salesUnit.OrderPosition)))
                    return true;

                return false;
            }
        }
    }
}