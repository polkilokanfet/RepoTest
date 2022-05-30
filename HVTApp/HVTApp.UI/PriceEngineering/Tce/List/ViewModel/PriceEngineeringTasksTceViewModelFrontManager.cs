using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.List.ViewModel
{
    public class PriceEngineeringTasksTceViewModelFrontManager : PriceEngineeringTasksTceViewModel
    {
        public PriceEngineeringTasksTceViewModelFrontManager(IUnityContainer container) : base(container)
        {
        }

        protected override bool TaskIsActual(PriceEngineeringTasks tasks)
        {
            return tasks.UserManager?.Id == GlobalAppProperties.User.Id;
        }
    }
}