using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin,
        SalesManager,
        Economist,
        DataBaseFiller,
        Director
    }

}