using System.Data.Entity;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class ProductParametersRepository : BaseRepository<ProductParameter>, IProductParametersRepository {
        public ProductParametersRepository(DbContext context) : base(context)
        {
        }
    }
}