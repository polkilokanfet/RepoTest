using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.List.ViewModel
{
    public class PriceEngineeringTasksTceViewModelBackManagerBoss : PriceEngineeringTasksTceViewModel
    {
        public PriceEngineeringTasksTceViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
        }

        protected override bool TaskIsActual(PriceEngineeringTaskTce task)
        {
            return true;
        }
    }
}