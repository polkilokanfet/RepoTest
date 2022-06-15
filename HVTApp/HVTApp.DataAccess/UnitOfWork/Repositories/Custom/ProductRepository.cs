using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductRepository
    {
        protected override IQueryable<Product> GetQuary()
        {
            return Context.Set<Product>().AsQueryable()
                .Include(x => x.ProductBlock)
                .Include(x => x.DependentProducts);
        }

        public override UnitOfWorkOperationResult Add(Product product)
        {
            var checkResult = Check(product);
            if (checkResult.OperationCompletedSuccessfully == false)
            {
                return checkResult;
            }

            return base.Add(product);
        }

        public override UnitOfWorkOperationResult AddRange(IEnumerable<Product> products)
        {
            var results = new List<UnitOfWorkOperationResult>();
            foreach (var product in products)
            {
                results.Add(this.Add(product));
            }

            if (results.All(x => x.OperationCompletedSuccessfully))
            {
                return new UnitOfWorkOperationResult();
            }

            return new UnitOfWorkOperationResult(new Exception("Не все продукты удалось добавить в репозиторий"));
        }

        private UnitOfWorkOperationResult Check(Product product)
        {
            if (this.GetById(product.Id) != null)
            {
                return new UnitOfWorkOperationResult(new Exception("Product с таким Id уже присутствует в репозитории"));
            }

            if (this.FindAsNoTracking(x => x.Equals(product)).Any())
            {
                return new UnitOfWorkOperationResult(new Exception("Product с таким набором (ProductBlock + DependentProducts) уже присутствует в репозитории"));
            }

            return new UnitOfWorkOperationResult();
        }
    }
}
