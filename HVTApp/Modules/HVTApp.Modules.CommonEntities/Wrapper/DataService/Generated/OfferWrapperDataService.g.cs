using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapperDataService : WrapperDataService<Offer, OfferWrapper>
    {
        public OfferWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OfferWrapper GenerateWrapper(Offer model)
        {
            return new OfferWrapper(model);
        }
    }
}
