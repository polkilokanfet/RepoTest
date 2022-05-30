using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.List.ViewModel
{
    public class PriceEngineeringTasksTceViewModelBackManagerBoss : PriceEngineeringTasksTceViewModel
    {
        public PriceEngineeringTasksTceViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
        }

        protected override bool TaskIsActual(PriceEngineeringTasks tasks)
        {
            if (tasks.BackManager != null)
                return true;

            return tasks.PriceCalculations.Any(x => x.IsTceConnected && x.LastHistoryItem?.Type == PriceCalculationHistoryItemType.Create);
        }
    }
}