using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProjectUnitRepository : BaseRepository<ProjectUnit>, IProjectUnitRepository
    {
        public ProjectUnitRepository(DbContext context) : base(context)
        {
        }
    }
}
