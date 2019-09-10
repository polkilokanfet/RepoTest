namespace HVTApp.DataAccess
{
    public partial class CountryUnionConfiguration
    {
        public CountryUnionConfiguration()
        {
            HasMany(x => x.Countries).WithOptional().WillCascadeOnDelete(false);
        }
    }
}