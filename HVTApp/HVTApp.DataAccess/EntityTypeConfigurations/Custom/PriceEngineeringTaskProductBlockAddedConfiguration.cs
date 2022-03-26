namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTaskProductBlockAddedConfiguration
    {
        public PriceEngineeringTaskProductBlockAddedConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany().WillCascadeOnDelete(false);
        }
    }
}