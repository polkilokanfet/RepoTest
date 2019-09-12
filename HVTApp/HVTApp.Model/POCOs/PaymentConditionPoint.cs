using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Точка отсчета условия платежа.
    /// </summary>
    [Designation("Условие платежа (точка отсчета)")]
    public class PaymentConditionPoint : BaseEntity
    {
        [Designation("Название"), Required, OrderStatus(6)]
        public string Name { get; set; }

        public virtual PaymentConditionPointEnum PaymentConditionPointEnum { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum PaymentConditionPointEnum
    {
        /// <summary>
        /// Начало производства.
        /// </summary>
        ProductionStart,
        /// <summary>
        /// Окончание производства.
        /// </summary>
        ProductionEnd,
        /// <summary>
        /// Отгрузка.
        /// </summary>
        Shipment,
        /// <summary>
        /// Доставка.
        /// </summary>
        Delivery
    }
}