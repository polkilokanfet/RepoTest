using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class LocalityTypeWrapperDataService : WrapperDataService<LocalityType, LocalityTypeWrapper>
    {
        public LocalityTypeWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override LocalityTypeWrapper GenerateWrapper(LocalityType model)
        {
            return new LocalityTypeWrapper(model);
        }
    }
}
