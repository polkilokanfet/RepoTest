using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class RegionWrapperDataService : WrapperDataService<Region, RegionWrapper>
    {
        public RegionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override RegionWrapper GenerateWrapper(Region model)
        {
            return new RegionWrapper(model);
        }
    }
}
