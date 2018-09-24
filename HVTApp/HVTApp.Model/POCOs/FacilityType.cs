using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��� �������")]
    [AllowEdit(Role.SalesManager)]
    public partial class FacilityType : BaseEntity
    {
        [Designation("������������"), Required, MaxLength(50)]
        public string FullName { get; set; }

        [Designation("����������� ������������"), MaxLength(10)]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, {ShortName}";
        }
    }
}