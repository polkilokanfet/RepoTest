using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class ProductRepository
    {
        private Product Designate(Product product)
        {
            var designator = _container.Resolve<IProductDesignationService>();
            product.Designation = product.DesignationSpecial ?? designator.GetDesignation(product);
            product.ProductType = designator.GetProductType(product);
            return product;
        }

        private List<Product> Designate(List<Product> products)
        {
            products.ForEach(x => Designate(x));
            return products;
        }

        public override async Task<List<Product>> GetAllAsync()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var products = await Context.Set<Product>().AsQueryable().Include(x => x.ProductBlock).Include(x => x.DependentProducts).ToListAsync();
            return Designate(products);
        }

        public override async Task<List<Product>> GetAllAsNoTrackingAsync()
        {
            return Designate(await base.GetAllAsNoTrackingAsync());
        }

        public override List<Product> Find(Func<Product, bool> predicate)
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var products = Context.Set<Product>().AsQueryable().Include(x => x.ProductBlock).Where(predicate).ToList();
            return Designate(products);
        }

        public override async Task<Product> GetByIdAsync(Guid id)
        {
            var product = (await base.GetByIdAsync(id));
            return product != null ? Designate(product) : null;
        }
    }
}
