using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Должность сотрудника.
    /// </summary>
    [Designation("Должность")]
    [AllowEdit(Role.DataBaseFiller, Role.SalesManager, Role.Economist)]
    public partial class EmployeesPosition : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(150)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}