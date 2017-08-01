using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProductItemConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductItemConfiguration()
        {
            Property(x => x.Designation).IsRequired().HasMaxLength(100);
            HasMany(x => x.Parameters).WithMany();
        }
    }
}