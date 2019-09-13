using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Роль пользователя")]
    public partial class UserRole : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(15)]
        public string Name { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}