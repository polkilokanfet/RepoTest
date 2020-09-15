using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Стоимость и ПЗ категории продукта")]
    [AllowEdit(Role.ReportMaker, Role.Director)]
    public class ProductCategoryPriceAndCost : BaseEntity
    {
        [Designation("Категория"), Required, OrderStatus(90)]
        public virtual ProductCategory Category { get; set; }

        [Designation("Стоимость"), Required, OrderStatus(80)]
        public double Cost { get; set; }

        [Designation("Себестоимость"), Required, OrderStatus(70)]
        public double Price { get; set; }

        [Designation("StructureCost"), MaxLength(50), OrderStatus(70)]
        public string StructureCost { get; set; }
    }
}