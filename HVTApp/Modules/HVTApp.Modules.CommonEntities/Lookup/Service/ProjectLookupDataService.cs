using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class ProjectLookupDataService : LookupDataService<ProjectLookup, Project>, IProjectLookupDataService
    {
        public ProjectLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
    }
}