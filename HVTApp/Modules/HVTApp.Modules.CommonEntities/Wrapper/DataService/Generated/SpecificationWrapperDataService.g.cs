using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class SpecificationWrapperDataService : WrapperDataService<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override SpecificationWrapper GenerateWrapper(Specification model)
        {
            return new SpecificationWrapper(model);
        }
    }
}
