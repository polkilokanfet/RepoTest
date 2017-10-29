using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapperDataService : WrapperDataService<OfferUnit, OfferUnitWrapper>
    {
        public OfferUnitWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override OfferUnitWrapper GenerateWrapper(OfferUnit model)
        {
            return new OfferUnitWrapper(model);
        }
    }
}
