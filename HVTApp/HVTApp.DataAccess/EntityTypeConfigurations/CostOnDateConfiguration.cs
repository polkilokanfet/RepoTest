using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class CostOnDateConfiguration : EntityTypeConfiguration<CostOnDate>
    {
        public CostOnDateConfiguration()
        {
            Property(x => x.Cost).IsRequired();
            Property(x => x.Date).IsRequired();
        }
    }
}