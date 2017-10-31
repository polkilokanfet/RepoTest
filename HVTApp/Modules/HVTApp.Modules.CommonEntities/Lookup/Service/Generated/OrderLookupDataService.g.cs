using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OrderLookupDataService : LookupDataService<OrderLookup, Order>, IOrderLookupDataService
    {
        public OrderLookupDataService(HvtAppContext context) : base(context) { }
    }
}
