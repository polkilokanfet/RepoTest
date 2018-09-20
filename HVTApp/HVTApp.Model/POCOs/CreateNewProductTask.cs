using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Задание на создание нового продукта")]
    public partial class CreateNewProductTask : BaseEntity
    {
        [Designation("Обозначение")]
        public string Designation { get; set; }

        [Designation("Сралчахвост")]
        public string StructureCostNumber { get; set; }

        [Designation("Продукт")]
        public virtual Product Product { get; set; }
    }
}