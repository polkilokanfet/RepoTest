using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class DistrictWrapperDataService : WrapperDataService<District, DistrictWrapper>
    {
        public DistrictWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override DistrictWrapper GenerateWrapper(District model)
        {
            return new DistrictWrapper(model);
        }
    }
}
