using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductsRelationRepository : BaseRepository<ProductsRelation>, IProductsRelationRepository
    {
        public ProductsRelationRepository(DbContext context) : base(context)
        {
        }
    }
}
