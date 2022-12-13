using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.EngineeringDepartmentTasksQueue.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public class EngineeringDepartmentTasksQueueViewModelConstructor : EngineeringDepartmentTasksQueueViewModel
    {
        public EngineeringDepartmentTasksQueueViewModelConstructor(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<EngineeringDepartmentTask> GetAllItems()
        {
            return UnitOfWork.Repository<PriceEngineeringTask>()
                .Find(x => x.UserConstructor != null && x.UserConstructor.Id == GlobalAppProperties.User.Id)
                .Where(x => x.IsInProcessByConstructor)
                .Select(GetTopTask)
                .Distinct()
                .Where(x => x.SalesUnits.Any())
                .Select(x => new EngineeringDepartmentTaskPrice(x, UnitOfWork.Repository<PriceEngineeringTasks>().GetById(x.ParentPriceEngineeringTasksId.Value).WorkUpTo))
                .OrderBy(x => x);
        }
    }
}