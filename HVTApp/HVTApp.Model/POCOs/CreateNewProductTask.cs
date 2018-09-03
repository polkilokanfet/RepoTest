using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Задание на создание нового продукта")]
    public class CreateNewProductTask : BaseEntity
    {
        [Designation("Обозначение")]
        public string Designation { get; set; }
        [Designation("Сралчахвост")]
        public string StructureCostNumber { get; set; }
        [Designation("Продукт")]
        public virtual Product Product { get; set; }
    }
}