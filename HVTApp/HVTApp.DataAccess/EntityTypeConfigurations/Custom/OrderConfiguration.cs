using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(x => x.Number).IsRequired().HasMaxLength(10);
            Property(x => x.OpenOrderDate).IsRequired();
            HasMany(x => x.ProductionUnits).WithOptional().HasForeignKey(x => x.OrderId);
        }
    }
}