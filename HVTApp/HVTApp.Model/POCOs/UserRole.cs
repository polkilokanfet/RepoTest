using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return Name;
        }
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