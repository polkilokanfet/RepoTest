using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelDesignDepartmentHead : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemDesignDepartmentHead, PriceEngineeringTaskListItemDesignDepartmentHead>
    {
        public PriceEngineeringTasksListViewModelDesignDepartmentHead(IUnityContainer container) : base(container)
        {
        }

        protected override PriceEngineeringTasksListItemDesignDepartmentHead GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemDesignDepartmentHead(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();
        }
    }
}