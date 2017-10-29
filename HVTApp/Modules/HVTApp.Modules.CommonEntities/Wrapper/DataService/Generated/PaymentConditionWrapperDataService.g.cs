using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentConditionWrapperDataService : WrapperDataService<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override PaymentConditionWrapper GenerateWrapper(PaymentCondition model)
        {
            return new PaymentConditionWrapper(model);
        }
    }
}
