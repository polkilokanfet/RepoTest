using System.Data.Entity;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class ProjectsRepository : BaseRepository<Project, ProjectWrapper>, IProjectsRepository {
        public ProjectsRepository(DbContext context) : base(context)
        {
        }
    }
}