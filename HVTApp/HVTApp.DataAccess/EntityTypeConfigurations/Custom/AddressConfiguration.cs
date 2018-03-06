namespace HVTApp.DataAccess
{
    public partial class AddressConfiguration
    {
        public AddressConfiguration()
        {
            Property(x => x.Description).IsRequired().HasMaxLength(150);
            HasRequired(x => x.Locality).WithMany();
        }
    }
}