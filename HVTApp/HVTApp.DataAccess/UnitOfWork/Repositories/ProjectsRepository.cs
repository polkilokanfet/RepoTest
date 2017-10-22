using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess
{
    public class ProjectsRepository : BaseRepository<Project>, IProjectsRepository
    {
        public ProjectsRepository(DbContext context) : base(context)
        {
        }
    }
}