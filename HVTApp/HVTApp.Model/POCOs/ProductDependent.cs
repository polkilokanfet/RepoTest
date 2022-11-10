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

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ MainProductId.GetHashCode();
                hashCode = (hashCode * 397) ^ (Product != null ? Product.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Amount;
                return hashCode;
            }
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