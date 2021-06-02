namespace HVTApp.DataAccess
{
    public partial class LaborHoursConfiguration
    {
        public LaborHoursConfiguration()
        {
            HasMany(laborHours => laborHours.Parameters).WithMany();
        }
    }
}