namespace HVTApp.DataAccess
{
    public partial class CountryConfiguration 
    {
        public CountryConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
        }
    }
}