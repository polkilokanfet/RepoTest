using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ParametersGroupsRepository : BaseRepository<ParameterGroup>, IParametersGroupsRepository {
        public ParametersGroupsRepository(DbContext context) : base(context)
        {
        }
    }
}