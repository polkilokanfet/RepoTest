using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess
{
    public class ProjectUnitsRepository : BaseRepository<ProjectUnit>, IProjectUnitsRepository
    {
        public ProjectUnitsRepository(DbContext context) : base(context)
        {
        }
    }
}