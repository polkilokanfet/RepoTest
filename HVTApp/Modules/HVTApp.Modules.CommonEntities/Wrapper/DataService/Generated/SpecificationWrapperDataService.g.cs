using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class SpecificationWrapperDataService : WrapperDataService<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override SpecificationWrapper GenerateWrapper(Specification model)
        {
            return new SpecificationWrapper(model);
        }
    }
}
