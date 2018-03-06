namespace HVTApp.DataAccess
{
    public partial class CostOnDateConfiguration
    {
        public CostOnDateConfiguration()
        {
            Property(x => x.Cost).IsRequired();
            Property(x => x.Date).IsRequired();
        }
    }
}