using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProductConfiguration : EntityTypeConfiguration<Equipment>
    {
        public ProductConfiguration()
        {
            Property(x => x.Designation).IsRequired().HasMaxLength(100);
            HasRequired(x => x.Product).WithMany();
            HasMany(x => x.DependentEquipments).WithMany();
        }
    }
}