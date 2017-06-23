using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProductsRepository : BaseRepository<Product, ProductWrapper>, IProductsRepository
    {
        public ProductsRepository(DbContext context) : base(context)
        {
        }
    }

    public class RequiredChildProductParametersRepository : BaseRepository<RequiredChildProductParameters, RequiredChildProductParametersWrapper>, IRequiredChildProductParametersRepository
    {
        public RequiredChildProductParametersRepository(DbContext context) : base(context)
        {
        }
    }
}