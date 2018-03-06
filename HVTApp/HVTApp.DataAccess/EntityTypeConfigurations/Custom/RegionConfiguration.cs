namespace HVTApp.DataAccess
{
    public partial class RegionConfiguration 
    {
        public RegionConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasRequired(x => x.District).WithMany();
        }
    }
}