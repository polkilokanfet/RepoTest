using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentDocumentWrapperDataService : WrapperDataService<PaymentDocument, PaymentDocumentWrapper>
    {
        public PaymentDocumentWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override PaymentDocumentWrapper GenerateWrapper(PaymentDocument model)
        {
            return new PaymentDocumentWrapper(model);
        }
    }
}
