using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestWifeWrapperDataService : WrapperDataService<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestWifeWrapper GenerateWrapper(TestWife model)
        {
            return new TestWifeWrapper(model);
        }
    }
}
