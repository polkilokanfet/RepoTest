using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProjectUnitsRepository : BaseRepository<ProjectUnit, ProjectUnitWrapper>, IProjectUnitsRepository
    {
        public ProjectUnitsRepository(DbContext context, IGetWrapper wrappersGetter) : base(context, wrappersGetter)
        {
        }
    }
}