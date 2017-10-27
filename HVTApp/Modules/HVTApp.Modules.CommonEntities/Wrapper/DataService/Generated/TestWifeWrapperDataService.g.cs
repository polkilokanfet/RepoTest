using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestWifeWrapperDataService : WrapperDataService<TestWife, TestWifeWrapper>
    {
        public TestWifeWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override TestWifeWrapper GenerateWrapper(TestWife model)
        {
            return new TestWifeWrapper(model);
        }
    }
}
