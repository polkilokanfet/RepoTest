using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class LocalityConfiguration : EntityTypeConfiguration<Locality>
    {
        public LocalityConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.StandartDeliveryPeriod).IsOptional();
            HasRequired(x => x.LocalityType).WithMany();
        }
    }
}