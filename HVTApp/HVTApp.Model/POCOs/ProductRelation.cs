using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Параметры обязательных дочерних продуктов.
    /// </summary>
    [Designation("Связи продуктов")]
    public partial class ProductRelation : BaseEntity
    {
        [Designation("Имя"), MaxLength(75), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Родительские параметры"), OrderStatus(8)]
        public virtual List<Parameter> ParentProductParameters { get; set; } = new List<Parameter>();

        [Designation("Дочерние параметры"), OrderStatus(6)]
        public virtual List<Parameter> ChildProductParameters { get; set; } = new List<Parameter>();

        [Designation("Количество дочерних продуктов"), OrderStatus(4), Required]
        public int ChildProductsAmount { get; set; } = 1;

        [Designation("Уникальность")]
        public bool IsUnique { get; set; } = true;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(ProductRelation other)
        {
            return base.Equals(other) ||
                   (ParentProductParameters.MembersAreSame(other.ParentProductParameters) &&
                    ChildProductParameters.MembersAreSame(other.ChildProductParameters) &&
                    ChildProductsAmount == other.ChildProductsAmount &&
                    IsUnique == other.IsUnique);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = ParentProductParameters != null ? ParentProductParameters.GetHashSum() : 0;
                hashCode = (hashCode * 397) ^ (ChildProductParameters != null ? ChildProductParameters.GetHashSum() : 0);
                hashCode = (hashCode * 397) ^ ChildProductsAmount;
                hashCode = (hashCode * 397) ^ IsUnique.GetHashCode();
                return hashCode;
            }
        }
    }
}