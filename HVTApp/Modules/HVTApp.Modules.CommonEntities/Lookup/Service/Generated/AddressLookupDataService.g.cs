using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class AddressLookupDataService : LookupDataService<AddressLookup, Address>, IAddressLookupDataService
    {
        public AddressLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
