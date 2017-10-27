using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class DocumentsRegistrationDetailsWrapperDataService : WrapperDataService<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>
    {
        public DocumentsRegistrationDetailsWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override DocumentsRegistrationDetailsWrapper GenerateWrapper(DocumentsRegistrationDetails model)
        {
            return new DocumentsRegistrationDetailsWrapper(model);
        }
    }
}
