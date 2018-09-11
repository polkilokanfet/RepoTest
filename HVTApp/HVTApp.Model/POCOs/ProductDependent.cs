using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Зависимое оборудование (например привод в выключателе).
    /// </summary>
    public partial class ProductDependent : BaseEntity
    {
        public virtual Guid MainProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Amount { get; set; } = 1;

        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;

            var other = obj as ProductDependent;
            if (other == null) return false;

            if (this.Amount != other.Amount) return false;

            return this.Product.Equals(other.Product);
        }
    }
}