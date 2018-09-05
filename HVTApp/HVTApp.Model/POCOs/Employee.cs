using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Сотрудник")]
    [DesignationPlural("Сотрудники")]
    public class Employee : BaseEntity
    {
        [Designation("Персона")]
        public virtual Person Person { get; set; }

        [Designation("Телефон")]
        public string PhoneNumber { get; set; }

        [Designation("e-mail")]
        public string Email { get; set; }

        [Designation("Компания")]
        public virtual Company Company { get; set; }

        [Designation("Должность")]
        public virtual EmployeesPosition Position { get; set; }

        public override string ToString()
        {
            return $"{Position} компании {Company}";
        }
    }
}
