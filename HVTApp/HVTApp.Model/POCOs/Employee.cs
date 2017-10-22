using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Employee : BaseEntity
    {
        public virtual Guid PersonId { get; set; }
        public bool IsActual { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Guid CompanyId { get; set; }
        public virtual EmployeesPosition Position { get; set; }
    }
}
