using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��� �������")]
    public partial class FacilityType : BaseEntity
    {
        [Designation("������������")]
        public string FullName { get; set; }

        [Designation("����������� ������������")]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, {ShortName}";
        }
    }
}