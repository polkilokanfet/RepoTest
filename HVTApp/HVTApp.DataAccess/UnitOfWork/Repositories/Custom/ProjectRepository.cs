using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProjectRepository
    {
        protected override IQueryable<Project> GetQuary()
        {
            return Context.Set<Project>().AsQueryable()
                .Include(x => x.ProjectType)
                .Include(x => x.Manager);
        }
    }
}