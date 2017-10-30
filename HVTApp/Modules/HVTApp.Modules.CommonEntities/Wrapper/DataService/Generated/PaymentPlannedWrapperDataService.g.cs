using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentPlannedWrapperDataService : WrapperDataService<PaymentPlanned, PaymentPlannedWrapper>
    {
        public PaymentPlannedWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentPlannedWrapper GenerateWrapper(PaymentPlanned model)
        {
            return new PaymentPlannedWrapper(model);
        }
    }
}
