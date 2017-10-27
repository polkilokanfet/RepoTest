using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapperDataService : WrapperDataService<OfferUnit, OfferUnitWrapper>
    {
        public OfferUnitWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override OfferUnitWrapper GenerateWrapper(OfferUnit model)
        {
            return new OfferUnitWrapper(model);
        }
    }
}
