using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class FacilityConfiguration : EntityTypeConfiguration<Facility>
    {
        public FacilityConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
            HasRequired(x => x.Type).WithMany();
            HasRequired(x => x.OwnerCompany).WithMany();
            HasOptional(x => x.Address).WithOptionalDependent();
        }
    }
}