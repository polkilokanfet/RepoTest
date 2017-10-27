using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class FacilityWrapperDataService : WrapperDataService<Facility, FacilityWrapper>
    {
        public FacilityWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override FacilityWrapper GenerateWrapper(Facility model)
        {
            return new FacilityWrapper(model);
        }
    }
}
