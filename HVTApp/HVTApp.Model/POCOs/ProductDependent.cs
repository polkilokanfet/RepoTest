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

        public override bool Equals(object other)
        {
            return base.Equals(other) || Equals(other as ProductDependent);
        }

        protected bool Equals(ProductDependent other)
        {
            return Amount == other?.Amount && this.Product.Equals(other.Product);
        }
    }
}