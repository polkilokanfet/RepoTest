using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferUnitLookupDataService : LookupDataService<OfferUnitLookup, OfferUnit>, IOfferUnitLookupDataService
    {
        public OfferUnitLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
