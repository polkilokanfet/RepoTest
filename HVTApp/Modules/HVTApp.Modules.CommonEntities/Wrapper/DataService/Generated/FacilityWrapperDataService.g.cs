using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class FacilityWrapperDataService : WrapperDataService<Facility, FacilityWrapper>
    {
        public FacilityWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override FacilityWrapper GenerateWrapper(Facility model)
        {
            return new FacilityWrapper(model);
        }
    }
}
