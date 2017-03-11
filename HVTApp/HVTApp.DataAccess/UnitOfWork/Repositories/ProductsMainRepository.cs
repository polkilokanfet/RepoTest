using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class ProductsMainRepository : BaseRepository<ProductMain>, IProductsMainRepository
    {
        public ProductsMainRepository(DbContext context) : base(context)
        {
        }

        public override List<ProductMain> GetAll()
        {
            return Context.Set<ProductMain>()
                .Include(nameof(ProductMain.CostInfo))
                //.Include(nameof(ProductMain.PaymentsActual))
                //.Include(nameof(ProductMain.PaymentsConditions))
                .ToList();
        }
    }
}