namespace HVTApp.DataAccess
{
    public partial class RegionConfiguration 
    {
        public RegionConfiguration()
        {
            HasRequired(x => x.District).WithMany();
        }
    }
}