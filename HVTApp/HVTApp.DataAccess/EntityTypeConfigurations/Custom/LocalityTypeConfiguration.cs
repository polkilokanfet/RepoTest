namespace HVTApp.DataAccess
{
    public partial class LocalityTypeConfiguration 
    {
        public LocalityTypeConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(75);
            Property(x => x.ShortName).IsRequired().HasMaxLength(20);
        }
    }
}