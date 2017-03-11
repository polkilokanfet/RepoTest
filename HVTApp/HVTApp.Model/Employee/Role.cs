namespace HVTApp.Model
{
    public class UserRole : BaseEntity
    {
        public Role Role { get; set; }
        public virtual User User { get; set; }

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