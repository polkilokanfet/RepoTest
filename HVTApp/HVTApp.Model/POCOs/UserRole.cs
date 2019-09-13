using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("���� ������������")]
    public partial class UserRole : BaseEntity
    {
        [Designation("��������"), Required, MaxLength(15)]
        public string Name { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}