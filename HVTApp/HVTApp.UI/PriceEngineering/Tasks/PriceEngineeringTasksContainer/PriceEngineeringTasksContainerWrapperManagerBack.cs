using System.Collections.Generic;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class PriceEngineeringTasksContainerWrapperManagerBack : PriceEngineeringTasksContainerWrapper<TaskViewModelManagerBack>
    {
        public PriceEngineeringTasksContainerWrapperManagerBack(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override IEnumerable<TaskViewModelManagerBack> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            foreach (var priceEngineeringTask in this.Model.ChildPriceEngineeringTasks)
            {
                yield return new TaskViewModelManagerBack(container, priceEngineeringTask.Id);
            }
        }
    }
}