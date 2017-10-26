using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class TenderTypeLookupDataService : LookupDataService<TenderTypeLookup, TenderType>, ITenderTypeLookupDataService
    {
        public TenderTypeLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
