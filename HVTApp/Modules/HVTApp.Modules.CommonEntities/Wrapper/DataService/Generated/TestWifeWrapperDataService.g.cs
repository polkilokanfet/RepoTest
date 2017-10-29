using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestWifeWrapperDataService : WrapperDataService<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TestWifeWrapper GenerateWrapper(TestWife model)
        {
            return new TestWifeWrapper(model);
        }
    }
}
