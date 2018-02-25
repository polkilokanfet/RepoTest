using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Employee : BaseEntity
    {
        public virtual Person Person { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Company Company { get; set; }
        public virtual EmployeesPosition Position { get; set; }

        public override string ToString()
        {
            return $"{Position} компании {Company}";
        }
    }
}
