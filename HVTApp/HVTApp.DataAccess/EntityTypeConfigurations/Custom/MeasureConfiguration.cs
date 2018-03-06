namespace HVTApp.DataAccess
{
    public partial class MeasureConfiguration
    {
        public MeasureConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.ShortName).IsOptional().HasMaxLength(50);
        }
    }
}