using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(x => x.Designation).IsRequired().HasMaxLength(100);
            HasMany(x => x.Parameters).WithMany();
            HasMany(x => x.DependentProducts).WithMany();
        }
    }
}