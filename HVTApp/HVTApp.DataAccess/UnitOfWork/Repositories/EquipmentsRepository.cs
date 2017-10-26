using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        public ProductsRepository(DbContext context) : base(context)
        {
        }
    }
}