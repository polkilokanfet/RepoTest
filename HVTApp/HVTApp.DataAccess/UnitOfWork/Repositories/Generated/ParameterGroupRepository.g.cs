using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterGroupRepository : BaseRepository<ParameterGroup>, IParameterGroupRepository
    {
        public ParameterGroupRepository(DbContext context) : base(context)
        {
        }
    }
}
