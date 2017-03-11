namespace HVTApp.Model
{
    public class Employee : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual Company Company { get; set; }
        public virtual EmployeesPosition Position { get; set; }
    }
}
