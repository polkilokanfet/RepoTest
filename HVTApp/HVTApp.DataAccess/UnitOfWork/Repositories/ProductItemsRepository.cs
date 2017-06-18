using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProductItemsRepository : BaseRepository<ProductItem, ProductItemWrapper>, IProductItemsRepository
    {
        public ProductItemsRepository(DbContext context) : base(context)
        {
        }

        public ProductItemWrapper GetProductItem(IEnumerable<ParameterWrapper> parameters)
        {
            var prmtrs = parameters.ToList();
            var productItems = this.GetAll();
            var result = productItems.FirstOrDefault(x => !x.Parameters.Except(prmtrs).Any() &&
                                                          !prmtrs.Except(x.Parameters).Any());
            if (result != null) return result;

            result = WrappersFactory.GetWrapper<ProductItem, ProductItemWrapper>(new ProductItem { Parameters = new List<Parameter>(prmtrs.Select(x => x.Model)) });
            result.Designation = result.ParametersToString;
            return result;
        }
    }
}