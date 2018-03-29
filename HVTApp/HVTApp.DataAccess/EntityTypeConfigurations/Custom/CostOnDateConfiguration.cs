namespace HVTApp.DataAccess
{
    public partial class CostOnDateConfiguration
    {
        public CostOnDateConfiguration()
        {
            HasRequired(x => x.Sum).WithOptional();
            Property(x => x.Date).IsRequired();
        }
    }
}