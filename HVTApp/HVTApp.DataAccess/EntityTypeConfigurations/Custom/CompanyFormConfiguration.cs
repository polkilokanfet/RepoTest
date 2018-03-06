namespace HVTApp.DataAccess
{
    public partial class CompanyFormConfiguration
    {
        public CompanyFormConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.ShortName).IsRequired().HasMaxLength(50);
        }
    }
}