using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper
{
    public class PaymentConditionSetEmptyWrapper : WrapperBase<PaymentConditionSet>
    {
        public PaymentConditionSetEmptyWrapper(PaymentConditionSet model) : base(model)
        {
        }
    }
}