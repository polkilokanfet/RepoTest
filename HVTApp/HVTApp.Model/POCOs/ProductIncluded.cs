using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Добавленное оборудование к основному (например ЗИП к выключателю).
    /// </summary>
    public class ProductIncluded: BaseEntity
    {
        public virtual Product Product { get; set; }
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