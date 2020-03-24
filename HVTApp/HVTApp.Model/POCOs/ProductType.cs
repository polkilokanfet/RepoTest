using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тип продукта")]
    public partial class ProductType : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(150)]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}