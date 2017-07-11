using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Должность сотрудника.
    /// </summary>
    public class EmployeesPosition : BaseEntity
    {
        public string Name { get; set; }
    }
}