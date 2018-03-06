namespace HVTApp.DataAccess
{
    public partial class FacilityTypeConfiguration
    {
        public FacilityTypeConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.ShortName).IsOptional().HasMaxLength(50);
        }
    }
}