using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterRepository : BaseRepository<Parameter>, IParameterRepository
    {
        public ParameterRepository(DbContext context) : base(context)
        {
        }
    }
}
