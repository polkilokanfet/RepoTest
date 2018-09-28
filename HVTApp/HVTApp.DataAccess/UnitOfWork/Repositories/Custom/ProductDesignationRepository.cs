using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductDesignationRepository
    {
        protected override IQueryable<ProductDesignation> GetQuary()
        {
            return Context.Set<ProductDesignation>().AsQueryable().Include(x => x.Parameters);
        }
    }
}