using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class TenderLookupDataService : LookupDataService<TenderLookup, Tender>, ITenderLookupDataService
    {
        public TenderLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
