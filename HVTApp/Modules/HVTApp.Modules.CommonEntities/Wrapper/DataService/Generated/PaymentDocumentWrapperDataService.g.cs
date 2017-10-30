using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentDocumentWrapperDataService : WrapperDataService<PaymentDocument, PaymentDocumentWrapper>
    {
        public PaymentDocumentWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override PaymentDocumentWrapper GenerateWrapper(PaymentDocument model)
        {
            return new PaymentDocumentWrapper(model);
        }
    }
}
