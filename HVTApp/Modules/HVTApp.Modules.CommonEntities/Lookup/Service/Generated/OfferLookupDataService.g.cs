using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookupDataService : LookupDataService<OfferLookup, Offer>, IOfferLookupDataService
    {
        public OfferLookupDataService(HvtAppContext context) : base(context) { }
    }
}
