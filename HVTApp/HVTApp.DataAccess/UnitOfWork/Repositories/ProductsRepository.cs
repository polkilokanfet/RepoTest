using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProductsRepository : BaseRepository<Product, ProductWrapper>, IProductItemsRepository
    {
        public ProductsRepository(DbContext context, IGetWrapper getWrapper) : base(context, getWrapper)
        {
        }

        public ProductWrapper GetProductItem(IEnumerable<ParameterWrapper> parameters)
        {
            var prmtrs = parameters.ToList();
            var productItems = this.GetAll();
            var result = productItems.FirstOrDefault(x => !x.Parameters.Except(prmtrs).Any() &&
                                                          !prmtrs.Except(x.Parameters).Any());
            if (result != null) return result;

            result = GetWrapper(new Product { Parameters = new List<Parameter>(prmtrs.Select(x => x.Model)) });
            result.Designation = result.ParametersToString;
            return result;
        }
    }
}