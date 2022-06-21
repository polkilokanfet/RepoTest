using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProjectRepository
    {
        protected override IQueryable<Project> GetQuery()
        {
            return Context.Set<Project>().AsQueryable()
                .Include(project => project.ProjectType)
                .Include(project => project.Manager);
        }

        public IEnumerable<Project> GetAllWithNotes()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return this.GetQuery().Include(x => x.Notes).ToList();
        }
    }

    public partial interface IProjectRepository
    {
        IEnumerable<Project> GetAllWithNotes();
    }
}