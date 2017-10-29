using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapperDataService : WrapperDataService<Offer, OfferWrapper>
    {
        public OfferWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override OfferWrapper GenerateWrapper(Offer model)
        {
            return new OfferWrapper(model);
        }
    }
}
