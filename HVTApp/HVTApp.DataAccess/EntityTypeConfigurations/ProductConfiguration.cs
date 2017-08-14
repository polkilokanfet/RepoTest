using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(x => x.Designation).IsRequired().HasMaxLength(100);
            HasRequired(x => x.Part).WithMany();
            HasMany(x => x.DependentProducts).WithMany();
        }
    }
}