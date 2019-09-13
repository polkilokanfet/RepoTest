using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ���������")]
    public class LosingReason : BaseEntity
    {
        [Designation("��������"), Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}