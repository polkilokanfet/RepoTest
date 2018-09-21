using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Сотрудник")]
    [DesignationPlural("Сотрудники")]
    public partial class Employee : BaseEntity
    {
        [Designation("Персона"), Required, OrderStatus(30)]
        public virtual Person Person { get; set; }

        [Designation("Телефон"), MaxLength(20), OrderStatus(20)]
        public string PhoneNumber { get; set; }

        [Designation("e-mail"), MaxLength(50), OrderStatus(10)]
        public string Email { get; set; }

        [Designation("Компания"), Required, OrderStatus(50)]
        public virtual Company Company { get; set; }

        [Designation("Должность"), Required, OrderStatus(40)]
        public virtual EmployeesPosition Position { get; set; }

        public override string ToString()
        {
            return $"{Position} компании {Company}";
        }
    }
}
