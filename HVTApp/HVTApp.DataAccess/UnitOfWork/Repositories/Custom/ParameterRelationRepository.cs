using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterRelationRepository
    {
        protected override IQueryable<ParameterRelation> GetQuary()
        {
            return Context.Set<ParameterRelation>()
                .Include(x => x.RequiredParameters);
        }
    }
}