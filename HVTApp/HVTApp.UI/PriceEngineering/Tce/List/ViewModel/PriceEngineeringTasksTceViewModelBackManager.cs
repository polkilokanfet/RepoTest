using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.List.ViewModel
{
    public class PriceEngineeringTasksTceViewModelBackManager: PriceEngineeringTasksTceViewModel
    {
        public PriceEngineeringTasksTceViewModelBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override bool TaskIsActual(PriceEngineeringTasks task)
        {
            return task.BackManager?.Id == GlobalAppProperties.User.Id;
        }
    }
}