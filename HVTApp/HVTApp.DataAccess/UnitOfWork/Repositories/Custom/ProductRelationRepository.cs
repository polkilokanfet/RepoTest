using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductRelationRepository
    {
        protected override IQueryable<ProductRelation> GetQuary()
        {
            return Context.Set<ProductRelation>().AsQueryable()
                .Include(x => x.ParentProductParameters)
                .Include(x => x.ChildProductParameters);
        }
    }
}