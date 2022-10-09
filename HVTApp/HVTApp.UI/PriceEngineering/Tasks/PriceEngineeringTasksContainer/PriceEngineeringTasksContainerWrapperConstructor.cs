using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class PriceEngineeringTasksContainerWrapperConstructor : PriceEngineeringTasksContainerWrapper<PriceEngineeringTaskViewModelConstructor>
    {
        public PriceEngineeringTasksContainerWrapperConstructor(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override IEnumerable<PriceEngineeringTaskViewModelConstructor> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            return Model.ChildPriceEngineeringTasks.Select(priceEngineeringTask => new PriceEngineeringTaskViewModelConstructor(container, priceEngineeringTask.Id));
        }
    }
}