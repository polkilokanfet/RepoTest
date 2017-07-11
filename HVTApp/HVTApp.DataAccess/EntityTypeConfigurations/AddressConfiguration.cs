using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(x => x.Description).IsRequired().HasMaxLength(150);
            HasRequired(x => x.Locality).WithMany();
        }
    }
}