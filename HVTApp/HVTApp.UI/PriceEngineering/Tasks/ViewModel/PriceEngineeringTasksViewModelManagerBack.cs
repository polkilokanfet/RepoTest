using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksViewModelManagerBack : PriceEngineeringTasksViewModelVisible<TasksWrapperManagerBack, TaskViewModelManagerBack>
    {
        public PriceEngineeringTasksViewModelManagerBack(IUnityContainer container) : base(container)
        {
        }

        protected override TasksWrapperManagerBack GetPriceEngineeringTasksWrapper(
            PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            throw new NotImplementedException();
        }
    }
}