using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentConditionWrapperDataService : WrapperDataService<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override PaymentConditionWrapper GenerateWrapper(PaymentCondition model)
        {
            return new PaymentConditionWrapper(model);
        }
    }
}
