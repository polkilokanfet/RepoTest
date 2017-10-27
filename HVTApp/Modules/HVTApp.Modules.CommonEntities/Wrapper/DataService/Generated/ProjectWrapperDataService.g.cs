using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapperDataService : WrapperDataService<Project, ProjectWrapper>
    {
        public ProjectWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override ProjectWrapper GenerateWrapper(Project model)
        {
            return new ProjectWrapper(model);
        }
    }
}
