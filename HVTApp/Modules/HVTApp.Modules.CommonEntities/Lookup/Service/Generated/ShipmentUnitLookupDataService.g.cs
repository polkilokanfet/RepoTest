using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class ShipmentUnitLookupDataService : LookupDataService<ShipmentUnitLookup, ShipmentUnit>, IShipmentUnitLookupDataService
    {
        public ShipmentUnitLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
