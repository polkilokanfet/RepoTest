using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class ProductRepository : BaseRepository<Product>, IProductRepository
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
            return Designate(await base.GetAllAsync());
        }

        public override async Task<List<Product>> GetAllAsNoTrackingAsync()
        {
            return Designate(await base.GetAllAsNoTrackingAsync());
        }

        public override List<Product> Find(Func<Product, bool> predicate)
        {
            return Designate(base.Find(predicate));
        }

        public override async Task<Product> GetByIdAsync(Guid id)
        {
            var product = (await base.GetByIdAsync(id));
            return product != null ? Designate(product) : null;
        }
    }
}
