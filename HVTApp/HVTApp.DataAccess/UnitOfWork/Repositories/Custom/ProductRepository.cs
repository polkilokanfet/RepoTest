//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using HVTApp.Model.POCOs;
//using HVTApp.Services.ProductDesignationService;
//using Microsoft.Practices.Unity;

//namespace HVTApp.DataAccess
//{
//    public partial class ProductRepository : BaseRepository<Product>, IProductRepository
//    {
//        public override async Task<List<Product>> GetAllAsync()
//        {
//            var designator = _container.Resolve<IProductDesignationService>();
//            var products = await base.GetAllAsync();
//            products.ForEach(x => x.Designate(designator, Context));
//            return products;
//        }

//        public override async Task<List<Product>> GetAllAsNoTrackingAsync()
//        {
//            var designator = _container.Resolve<IProductDesignationService>();
//            var products = await base.GetAllAsNoTrackingAsync();
//            products.ForEach(x => x.Designate(designator, Context));
//            return products;
//        }

//        public override List<Product> Find(Func<Product, bool> predicate)
//        {
//            var designator = _container.Resolve<IProductDesignationService>();
//            var products = base.Find(predicate);
//            products.ForEach(x => x.Designate(designator, Context));
//            return products;
//        }

//        public override async Task<Product> GetByIdAsync(Guid id)
//        {
//            var designator = _container.Resolve<IProductDesignationService>();
//            return (await base.GetByIdAsync(id))?.Designate(designator, Context);
//        }
//    }

//    public static class ProductRepositoryExt
//    {
//        public static Product Designate(this Product product, IProductDesignationService designator, DbContext context)
//        {
//            product.Designation = product.DesignationSpecial ?? designator.GetDesignation(product);
//            var productType = designator.GetProductType(product);
//            if (productType != null) product.ProductType = context.Set<ProductType>().AsNoTracking().Single(x => x.Id == productType.Id);
//            return product;
//        }
//    }
//}
