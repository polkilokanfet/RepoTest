using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("���.������� (����)")]
    [DesignationPlural("����� ��")]
    public partial class TechnicalRequrementsFile : BaseEntity
    {
        [Designation("���"), Required, MaxLength(50), OrderStatus(20)]
        public string Name { get; set; }

        [Designation("�����������"), MaxLength(250), OrderStatus(10)]
        public string Comment { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}