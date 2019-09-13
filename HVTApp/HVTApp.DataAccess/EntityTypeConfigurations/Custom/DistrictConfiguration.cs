namespace HVTApp.DataAccess
{
    public partial class DistrictConfiguration
    {
        public DistrictConfiguration()
        {
            HasRequired(x => x.Country).WithMany().WillCascadeOnDelete(false);
        }
    }
}