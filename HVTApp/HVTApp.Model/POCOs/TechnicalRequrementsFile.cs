using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тех.задание (файл)")]
    [DesignationPlural("Файлы ТЗ")]
    public partial class TechnicalRequrementsFile : BaseEntity
    {
        [Designation("Имя"), Required, MaxLength(50), OrderStatus(20)]
        public string Name { get; set; }

        [Designation("Комментарий"), MaxLength(250), OrderStatus(10)]
        public string Comment { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}