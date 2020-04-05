namespace HVTApp.DataAccess
{
    public partial class ConstructorParametersListConfiguration
    {
        public ConstructorParametersListConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
        }
    }
}