using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksViewModelManagerBackBoss : PriceEngineeringTasksViewModelVisible<PriceEngineeringTasksContainerWrapperManagerBack, PriceEngineeringTaskViewModelManagerBack>
    {
        public PriceEngineeringTasksViewModelManagerBackBoss(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksContainerWrapperManagerBack GetPriceEngineeringTasksWrapper(
            PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new PriceEngineeringTasksContainerWrapperManagerBack(priceEngineeringTasks, container);
        }
    }
}