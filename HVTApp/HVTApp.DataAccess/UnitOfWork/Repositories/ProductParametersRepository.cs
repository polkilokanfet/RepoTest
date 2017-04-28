using System.Data.Entity;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class ProductParametersRepository : BaseRepository<Parameter, ParameterWrapper>, IProductParametersRepository {
        public ProductParametersRepository(DbContext context) : base(context)
        {
        }
    }
}