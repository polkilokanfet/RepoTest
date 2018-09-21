namespace HVTApp.DataAccess
{
    public partial class AddressConfiguration
    {
        public AddressConfiguration()
        {
            HasRequired(x => x.Locality).WithMany();
        }
    }
}