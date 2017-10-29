using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class EmployeesPositionWrapperDataService : WrapperDataService<EmployeesPosition, EmployeesPositionWrapper>
    {
        public EmployeesPositionWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override EmployeesPositionWrapper GenerateWrapper(EmployeesPosition model)
        {
            return new EmployeesPositionWrapper(model);
        }
    }
}
