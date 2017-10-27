using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestChildWrapperDataService : WrapperDataService<TestChild, TestChildWrapper>
    {
        public TestChildWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override TestChildWrapper GenerateWrapper(TestChild model)
        {
            return new TestChildWrapper(model);
        }
    }
}
