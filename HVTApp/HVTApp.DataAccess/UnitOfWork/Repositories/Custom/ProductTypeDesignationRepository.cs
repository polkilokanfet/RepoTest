using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductTypeDesignationRepository
    {
        protected override IQueryable<ProductTypeDesignation> GetQuery()
        {
            return Context.Set<ProductTypeDesignation>().AsQueryable()
                .Include(x => x.Parameters)
                .Include(x => x.ProductType);
        }
    }
}