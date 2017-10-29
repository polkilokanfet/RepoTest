using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class RegionWrapperDataService : WrapperDataService<Region, RegionWrapper>
    {
        public RegionWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override RegionWrapper GenerateWrapper(Region model)
        {
            return new RegionWrapper(model);
        }
    }
}
