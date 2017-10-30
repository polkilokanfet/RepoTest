using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapperDataService : WrapperDataService<OfferUnit, OfferUnitWrapper>
    {
        public OfferUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override OfferUnitWrapper GenerateWrapper(OfferUnit model)
        {
            return new OfferUnitWrapper(model);
        }
    }
}
