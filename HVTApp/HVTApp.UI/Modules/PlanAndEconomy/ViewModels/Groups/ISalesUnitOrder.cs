using System;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
{
    public interface ISalesUnitOrder
    {
        DateTime? SignalToStartProductionDone { get; set; }
        DateTime? EndProductionPlanDate { get; set; }
        OrderWrapper Order { get; set; }
    }
}