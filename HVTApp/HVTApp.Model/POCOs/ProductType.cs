using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тип продукта")]
    public class ProductType : BaseEntity
    {
        [Designation("Название")]
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}