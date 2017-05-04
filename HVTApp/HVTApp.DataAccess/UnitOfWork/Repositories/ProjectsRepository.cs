using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class ProjectsRepository : BaseRepository<Project, ProjectWrapper>, IProjectsRepository {
        public ProjectsRepository(DbContext context, Dictionary<IBaseEntity, object> wrappersRepository) : base(context, wrappersRepository)
        {
        }
    }
}