using System.Collections.Generic;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{
    public class AfterChangeSalesUnitsEvent : PubSubEvent<IEnumerable<SalesUnit>> { }
    public class AfterAddSalesUnitsEvent : PubSubEvent<IEnumerable<SalesUnit>> { }
}