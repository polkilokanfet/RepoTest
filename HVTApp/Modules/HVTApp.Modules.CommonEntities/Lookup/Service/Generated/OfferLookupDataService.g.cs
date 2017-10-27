using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookupDataService : LookupDataService<OfferLookup, Offer>, IOfferLookupDataService
    {
        public OfferLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
