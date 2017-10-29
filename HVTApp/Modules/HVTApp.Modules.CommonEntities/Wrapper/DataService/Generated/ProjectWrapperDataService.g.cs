using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapperDataService : WrapperDataService<Project, ProjectWrapper>
    {
        public ProjectWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override ProjectWrapper GenerateWrapper(Project model)
        {
            return new ProjectWrapper(model);
        }
    }
}
