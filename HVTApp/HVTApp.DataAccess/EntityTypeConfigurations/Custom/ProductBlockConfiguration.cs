using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductBlockConfiguration : EntityTypeConfiguration<ProductBlock>
    {
        public ProductBlockConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
        }
    }
}