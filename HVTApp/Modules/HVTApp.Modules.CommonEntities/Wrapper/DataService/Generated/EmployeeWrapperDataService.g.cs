using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class EmployeeWrapperDataService : WrapperDataService<Employee, EmployeeWrapper>
    {
        public EmployeeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override EmployeeWrapper GenerateWrapper(Employee model)
        {
            return new EmployeeWrapper(model);
        }
    }
}
