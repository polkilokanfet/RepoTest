using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentActualWrapperDataService : WrapperDataService<PaymentActual, PaymentActualWrapper>
    {
        public PaymentActualWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override PaymentActualWrapper GenerateWrapper(PaymentActual model)
        {
            return new PaymentActualWrapper(model);
        }
    }
}
