using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ����������� ������������ � ��������� (�������� ��� � �����������).
    /// </summary>
    [Designation("���������� � ��������� ������������")]
    public partial class ProductIncluded: BaseEntity
    {
        [Designation("�������"), Required, OrderStatus(10)]
        public virtual Product Product { get; set; }

        [Designation("����������"), Required, OrderStatus(5)]
        public int Amount { get; set; } = 1;

        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;

            var otherProductDependent = obj as ProductIncluded;
            if (otherProductDependent == null) return false;

            if (this.Amount != otherProductDependent.Amount) return false;

            return this.Product.Equals(otherProductDependent.Product);
        }
    }
}