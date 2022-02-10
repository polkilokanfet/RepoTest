namespace HVTApp.DataAccess
{
    public partial class EventServiceUnitConfiguration
    {
        public EventServiceUnitConfiguration()
        {
            HasRequired(eventServiceUnit => eventServiceUnit.User).WithMany();
        }
    }
}