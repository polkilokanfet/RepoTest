using System.Data.Entity;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class ProjectsRepository : BaseRepository<Project>, IProjectsRepository {
        public ProjectsRepository(DbContext context) : base(context)
        {
        }
    }
}