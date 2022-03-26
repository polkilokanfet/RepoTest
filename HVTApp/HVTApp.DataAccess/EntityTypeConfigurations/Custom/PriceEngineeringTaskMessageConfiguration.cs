namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTaskMessageConfiguration
    {
        public PriceEngineeringTaskMessageConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
        }
    }
}