using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProductsRepository : BaseRepository<Product, ProductWrapper>, IProductsRepository
    {
        public ProductsRepository(DbContext context, Dictionary<IBaseEntity, object> wrappersRepository) : base(context, wrappersRepository)
        {
        }

        public ProductWrapper Find(IEnumerable<ParameterWrapper> parameters)
        {
            var resultEnumerable = Find(x => !x.Parameters.Except(parameters).Any()).ToList();
            if (resultEnumerable.Any()) return resultEnumerable.Single();
            return new ProductWrapper(new Product(), WrappersRepository);
        }
    }
}