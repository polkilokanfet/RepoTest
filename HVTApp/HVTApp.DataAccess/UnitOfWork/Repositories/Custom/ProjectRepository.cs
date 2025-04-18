using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Model;
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

        public IEnumerable<Project> GetAllForUserWithNotes()
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return this.GetQuery()
                .Where(project => project.Manager.Id == GlobalAppProperties.User.Id)
                .Include(project => project.Notes).ToList();
        }

        public Project GetForEdit(Guid projectId)
        {
            Loging(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return Context.Set<Project>()
                .AsQueryable()
                .Include(project => project.SalesUnits)
                .SingleOrDefault(project => project.Id == projectId);
        }
    }

    public partial interface IProjectRepository
    {
        IEnumerable<Project> GetAllForUserWithNotes();
        Project GetForEdit(Guid projectId);
    }
}