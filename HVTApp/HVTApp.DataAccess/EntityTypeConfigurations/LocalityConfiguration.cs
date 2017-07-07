using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class LocalityConfiguration : EntityTypeConfiguration<Locality>
    {
        public LocalityConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasRequired(x => x.LocalityType);
            HasOptional(x => x.StandartDeliveryPeriod).WithOptionalPrincipal(x => x.Locality);
        }
    }
}