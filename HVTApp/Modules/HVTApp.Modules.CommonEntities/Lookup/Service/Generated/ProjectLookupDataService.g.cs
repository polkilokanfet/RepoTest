using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookupDataService : LookupDataService<ProjectLookup, Project>, IProjectLookupDataService
    {
        public ProjectLookupDataService(HvtAppContext context) : base(context) { }
    }
}
