using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterRepository
    {
        protected override IQueryable<Parameter> GetQuary()
        {
            return Context.Set<Parameter>().AsQueryable()
                .Include(x => x.ParameterGroup)
                .Include(x => x.ParameterRelations.Select(r => r.RequiredParameters))
                .Include(x => x.ParameterGroup.Measure);
        }
    }
}