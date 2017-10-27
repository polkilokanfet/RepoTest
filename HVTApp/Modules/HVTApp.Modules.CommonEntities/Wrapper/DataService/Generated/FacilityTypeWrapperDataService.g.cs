using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class FacilityTypeWrapperDataService : WrapperDataService<FacilityType, FacilityTypeWrapper>
    {
        public FacilityTypeWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override FacilityTypeWrapper GenerateWrapper(FacilityType model)
        {
            return new FacilityTypeWrapper(model);
        }
    }
}
