using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class DocumentsRegistrationDetailsLookupDataService : LookupDataService<DocumentsRegistrationDetailsLookup, DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsLookupDataService
    {
        public DocumentsRegistrationDetailsLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
