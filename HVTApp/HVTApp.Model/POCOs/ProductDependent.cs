using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Зависимое оборудование (например привод в выключателе).
    /// </summary>
    [Designation("Зависимое оборудование")]
    public partial class ProductDependent : BaseEntity
    {
        public virtual Guid MainProductId { get; set; }

        [Designation("Продукт"), Required, OrderStatus(10)]
        public virtual Product Product { get; set; }

        [Designation("Количество"), Required, OrderStatus(5)]
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
            return $"{Product} ({Amount} шт.)";
        }
    }
}