using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentsRegistrationDetailsLookupDataService : LookupDataService<DocumentsRegistrationDetailsLookup, DocumentsRegistrationDetails>, IDocumentsRegistrationDetailsLookupDataService
    {
        public DocumentsRegistrationDetailsLookupDataService(HvtAppContext context) : base(context) { }
    }
}
