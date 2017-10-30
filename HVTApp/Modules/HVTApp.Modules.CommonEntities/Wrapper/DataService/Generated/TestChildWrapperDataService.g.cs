using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestChildWrapperDataService : WrapperDataService<TestChild, TestChildWrapper>
    {
        public TestChildWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestChildWrapper GenerateWrapper(TestChild model)
        {
            return new TestChildWrapper(model);
        }
    }
}
