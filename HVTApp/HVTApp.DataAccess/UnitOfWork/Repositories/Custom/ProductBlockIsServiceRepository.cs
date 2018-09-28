using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductBlockIsServiceRepository
    {
        protected override IQueryable<ProductBlockIsService> GetQuary()
        {
            return Context.Set<ProductBlockIsService>().Include(x => x.Parameters);
        }
    }
}