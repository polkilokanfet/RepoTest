using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class EmployeesPositionWrapperDataService : WrapperDataService<EmployeesPosition, EmployeesPositionWrapper>
    {
        public EmployeesPositionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override EmployeesPositionWrapper GenerateWrapper(EmployeesPosition model)
        {
            return new EmployeesPositionWrapper(model);
        }
    }
}
