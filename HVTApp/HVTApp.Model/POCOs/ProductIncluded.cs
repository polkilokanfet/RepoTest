using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Добавленное оборудование к основному (например, ЗИП к выключателю).
    /// </summary>
    [Designation("Включенное в стоимость оборудование")]
    public partial class ProductIncluded : BaseEntity
    {
        [Designation("Продукт"), Required, OrderStatus(10)]
        public virtual Product Product { get; set; }

        [Designation("Количество"), Required, OrderStatus(5)]
        public int Amount { get; set; } = 1;

        /// <summary>
        /// Нестандартная себестоимость одной единицы включенного оборудования (для нестандартной стоимости шеф-монтажа).
        /// </summary>
        [Designation("Прайс на единицу"), OrderStatus(3)]
        public double? CustomFixedPrice { get; set; }

        public override string ToString()
        {
            return $"{Product} ({AmountOnUnit} шт.)";
        }

        /// <summary>
        /// Количество родительских единиц.
        /// </summary>
        [NotMapped]
        public int ParentsCount { get; set; } = 1;

        /// <summary>
        /// Количество на единицу родителя
        /// </summary>
        [NotMapped]
        public double AmountOnUnit => (double) Amount / ParentsCount;

        //public override bool Equals(object obj)
        //{
        //    if (base.Equals(obj)) return true;

        //    var otherProductDependent = obj as ProductIncluded;
        //    if (otherProductDependent == null) return false;

        //    if (this.Amount != otherProductDependent.Amount) return false;

        //    return this.Product.Equals(otherProductDependent.Product);
        //}
    }
}