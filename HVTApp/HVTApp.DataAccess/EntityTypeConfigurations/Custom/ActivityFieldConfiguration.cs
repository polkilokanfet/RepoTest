namespace HVTApp.DataAccess
{
    public partial class ActivityFieldConfiguration
    {
        public ActivityFieldConfiguration()
        {
            Property(x => x.ActivityFieldEnum).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}