using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ��������� ������������ (�������� ������ � �����������).
    /// </summary>
    [Designation("��������� ������������")]
    public partial class ProductDependent : BaseEntity
    {
        public virtual Guid MainProductId { get; set; }

        [Designation("�������"), Required, OrderStatus(10)]
        public virtual Product Product { get; set; }

        [Designation("����������"), Required, OrderStatus(5)]
        public int Amount { get; set; } = 1;

        public override bool Equals(object other)
        {
            return base.Equals(other) || Equals(other as ProductDependent);
        }

        protected bool Equals(ProductDependent other)
        {
            return Amount == other?.Amount && this.Product.Equals(other.Product);
        }

        public override string ToString()
        {
            return $"{Product} ({Amount} ��.)";
        }
    }
}