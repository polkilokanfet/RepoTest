using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestHusbandWrapperDataService : WrapperDataService<TestHusband, TestHusbandWrapper>
    {
        public TestHusbandWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestHusbandWrapper GenerateWrapper(TestHusband model)
        {
            return new TestHusbandWrapper(model);
        }
    }
}
