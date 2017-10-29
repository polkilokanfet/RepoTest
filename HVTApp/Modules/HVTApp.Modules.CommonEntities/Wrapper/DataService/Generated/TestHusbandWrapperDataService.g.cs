using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestHusbandWrapperDataService : WrapperDataService<TestHusband, TestHusbandWrapper>
    {
        public TestHusbandWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TestHusbandWrapper GenerateWrapper(TestHusband model)
        {
            return new TestHusbandWrapper(model);
        }
    }
}
