using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��������� � �� ��������� ��������")]
    [AllowEdit(Role.ReportMaker, Role.Director)]
    public class ProductCategoryPriceAndCost : BaseEntity
    {
        [Designation("���������"), Required, OrderStatus(90)]
        public virtual ProductCategory Category { get; set; }

        [Designation("���������"), Required, OrderStatus(80)]
        public double Cost { get; set; }

        [Designation("�������������"), Required, OrderStatus(70)]
        public double Price { get; set; }

        [Designation("StructureCost"), MaxLength(50), OrderStatus(70)]
        public string StructureCost { get; set; }
    }
}