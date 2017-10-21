using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class RequiredDependentEquipmentsParametersConfiguration : EntityTypeConfiguration<ProductsRelation>
    {
        public RequiredDependentEquipmentsParametersConfiguration()
        {
            HasMany(x => x.ParentProductParameters).WithMany();
            HasMany(x => x.ParentProductParameters).WithMany();
            Property(x => x.Count).IsRequired();
        }
    }
}