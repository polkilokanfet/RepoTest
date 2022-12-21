using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class PriceEngineeringTasksContainerWrapperManagerBack : PriceEngineeringTasksContainerWrapper<PriceEngineeringTaskViewModelManagerBack>
    {
        public PriceEngineeringTasksContainerWrapperManagerBack(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskViewModelManagerBack> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            foreach (var priceEngineeringTask in this.Model.ChildPriceEngineeringTasks)
            {
                yield return new PriceEngineeringTaskViewModelManagerBack(container, priceEngineeringTask.Id);
            }
        }
    }
}