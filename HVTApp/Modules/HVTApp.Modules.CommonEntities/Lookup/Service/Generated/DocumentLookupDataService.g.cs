using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentLookupDataService : LookupDataService<DocumentLookup, Document>, IDocumentLookupDataService
    {
        public DocumentLookupDataService(HvtAppContext context) : base(context) { }
    }
}
