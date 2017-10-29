using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class SpecificationWrapperDataService : WrapperDataService<Specification, SpecificationWrapper>
    {
        public SpecificationWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override SpecificationWrapper GenerateWrapper(Specification model)
        {
            return new SpecificationWrapper(model);
        }
    }
}
