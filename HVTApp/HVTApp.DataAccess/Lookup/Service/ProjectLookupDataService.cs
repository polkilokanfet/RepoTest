using System;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess.Lookup
{
    public class ProjectLookupDataService : LookupDataService<ProjectLookup, Project>, IProjectLookupDataService
    {
        public ProjectLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
    }
}