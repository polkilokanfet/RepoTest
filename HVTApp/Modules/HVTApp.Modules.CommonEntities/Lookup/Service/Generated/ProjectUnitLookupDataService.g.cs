using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectUnitLookupDataService : LookupDataService<ProjectUnitLookup, ProjectUnit>, IProjectUnitLookupDataService
    {
        public ProjectUnitLookupDataService(HvtAppContext context) : base(context) { }
    }
}
