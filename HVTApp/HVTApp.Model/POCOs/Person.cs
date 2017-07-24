using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Person : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public bool IsMan { get; set; }
        public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    }
}