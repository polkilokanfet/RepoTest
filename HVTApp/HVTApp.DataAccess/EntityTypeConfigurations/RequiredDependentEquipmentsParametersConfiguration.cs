using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class RequiredDependentEquipmentsParametersConfiguration : EntityTypeConfiguration<RequiredDependentEquipmentsParameters>
    {
        public RequiredDependentEquipmentsParametersConfiguration()
        {
            HasMany(x => x.MainProductParameters).WithMany();
            HasMany(x => x.MainProductParameters).WithMany();
            Property(x => x.Count).IsRequired();
        }
    }
}