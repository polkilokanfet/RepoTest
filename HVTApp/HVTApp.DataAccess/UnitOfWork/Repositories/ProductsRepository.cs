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

    public class RequiredProductsChildsesRepository : BaseRepository<RequiredProductsChilds, RequiredProductsChildsWrapper>, IRequiredProductsChildsesRepository
    {
        public RequiredProductsChildsesRepository(DbContext context) : base(context)
        {
        }
    }
}