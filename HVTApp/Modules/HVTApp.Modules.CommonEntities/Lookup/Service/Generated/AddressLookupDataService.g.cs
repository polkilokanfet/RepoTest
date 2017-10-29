using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class AddressLookupDataService : LookupDataService<AddressLookup, Address>, IAddressLookupDataService
    {
        public AddressLookupDataService(HvtAppContext context) : base(context) { }
    }
}
