using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentPlannedWrapperDataService : WrapperDataService<PaymentPlanned, PaymentPlannedWrapper>
    {
        public PaymentPlannedWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override PaymentPlannedWrapper GenerateWrapper(PaymentPlanned model)
        {
            return new PaymentPlannedWrapper(model);
        }
    }
}