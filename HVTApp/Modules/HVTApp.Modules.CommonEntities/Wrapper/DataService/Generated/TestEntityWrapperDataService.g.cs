using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestEntityWrapperDataService : WrapperDataService<TestEntity, TestEntityWrapper>
    {
        public TestEntityWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestEntityWrapper GenerateWrapper(TestEntity model)
        {
            return new TestEntityWrapper(model);
        }
    }
}
