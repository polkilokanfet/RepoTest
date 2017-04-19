using System.Data.Entity;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class ProductParametersRepository : BaseRepository<Parameter>, IProductParametersRepository {
        public ProductParametersRepository(DbContext context) : base(context)
        {
        }
    }
}