using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PartWrapperDataService : WrapperDataService<Part, PartWrapper>
    {
        public PartWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override PartWrapper GenerateWrapper(Part model)
        {
            return new PartWrapper(model);
        }
    }
}
