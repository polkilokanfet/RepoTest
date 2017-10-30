using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class DocumentWrapperDataService : WrapperDataService<Document, DocumentWrapper>
    {
        public DocumentWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override DocumentWrapper GenerateWrapper(Document model)
        {
            return new DocumentWrapper(model);
        }
    }
}
