using System.Collections.Generic;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class AfterSaveOrderItemsEvent : PubSubEvent<IEnumerable<SalesUnit>> { }
}