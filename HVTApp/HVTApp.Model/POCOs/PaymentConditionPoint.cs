using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ����� ������� ������� �������.
    /// </summary>
    [Designation("������� ������� (����� �������)")]
    public class PaymentConditionPoint : BaseEntity
    {
        [Designation("��������"), Required, OrderStatus(6)]
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
        /// ������ ������������.
        /// </summary>
        ProductionStart,
        /// <summary>
        /// ��������� ������������.
        /// </summary>
        ProductionEnd,
        /// <summary>
        /// ��������.
        /// </summary>
        Shipment,
        /// <summary>
        /// ��������.
        /// </summary>
        Delivery
    }
}