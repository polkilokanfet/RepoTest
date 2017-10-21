using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        public ProductsRepository(DbContext context) : base(context)
        {
        }
    }
}