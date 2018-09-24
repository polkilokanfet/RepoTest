using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Задание на создание нового продукта")]
    public partial class CreateNewProductTask : BaseEntity
    {
        [Designation("Обозначение"), MaxLength(50), Required, OrderStatus(10)]
        public string Designation { get; set; }

        [Designation("Сралчахвост"), MaxLength(10), Required, OrderStatus(8)]
        public string StructureCostNumber { get; set; }

        [Designation("Комментарий"), MaxLength(150), OrderStatus(6)]
        public string Comment { get; set; }

        [Designation("Продукт"), Required]
        public virtual Product Product { get; set; }
    }
}