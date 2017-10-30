using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentConditionWrapperDataService : WrapperDataService<PaymentCondition, PaymentConditionWrapper>
    {
        public PaymentConditionWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentConditionWrapper GenerateWrapper(PaymentCondition model)
        {
            return new PaymentConditionWrapper(model);
        }
    }
}
