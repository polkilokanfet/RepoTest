using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DocumentLookupDataService : LookupDataService<DocumentLookup, Document>, IDocumentLookupDataService
    {
        public DocumentLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}