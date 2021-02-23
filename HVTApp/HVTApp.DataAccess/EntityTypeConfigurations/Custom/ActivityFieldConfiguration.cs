namespace HVTApp.DataAccess
{
    public partial class ActivityFieldConfiguration
    {
        public ActivityFieldConfiguration()
        {
            Property(activityField => activityField.ActivityFieldEnum).IsRequired();
        }
    }
}