using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TenderTypeLookupDataService : LookupDataService<TenderTypeLookup, TenderType>, ITenderTypeLookupDataService
    {
        public TenderTypeLookupDataService(HvtAppContext context) : base(context) { }
    }
}
