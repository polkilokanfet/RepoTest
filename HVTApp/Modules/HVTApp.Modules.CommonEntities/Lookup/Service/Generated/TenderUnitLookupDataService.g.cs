using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TenderUnitLookupDataService : LookupDataService<TenderUnitLookup, TenderUnit>, ITenderUnitLookupDataService
    {
        public TenderUnitLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}