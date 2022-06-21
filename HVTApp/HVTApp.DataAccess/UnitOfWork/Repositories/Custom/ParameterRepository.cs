using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterRepository
    {
        protected override IQueryable<Parameter> GetQuery()
        {
            return Context.Set<Parameter>().AsQueryable()
                .Include(parameter => parameter.ParameterGroup)
                .Include(parameter => parameter.ParameterRelations.Select(parameterRelation => parameterRelation.RequiredParameters))
                .Include(parameter => parameter.ParameterGroup.Measure);
        }
    }
}