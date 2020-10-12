namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsConfiguration
    {
        public TechnicalRequrementsConfiguration()
        {
            HasMany(x => x.SalesUnits).WithMany();
            HasMany(x => x.Files).WithMany();
        }
    }
}