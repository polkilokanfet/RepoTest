using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectUnitWrapperDataService : WrapperDataService<ProjectUnit, ProjectUnitWrapper>
    {
        public ProjectUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProjectUnitWrapper GenerateWrapper(ProjectUnit model)
        {
            return new ProjectUnitWrapper(model);
        }
    }
}
