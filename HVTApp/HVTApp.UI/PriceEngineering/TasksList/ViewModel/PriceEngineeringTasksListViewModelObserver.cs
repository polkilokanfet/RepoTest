using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelObserver : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemObserver, PriceEngineeringTaskListItemObserver>
    {
        public PriceEngineeringTasksListViewModelObserver(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksListItemObserver GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemObserver(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks
                .ChildPriceEngineeringTasks
                .Any(task => task.DesignDepartment != null && 
                             task.DesignDepartment.Observers.ContainsById(GlobalAppProperties.User));
        }
    }
}