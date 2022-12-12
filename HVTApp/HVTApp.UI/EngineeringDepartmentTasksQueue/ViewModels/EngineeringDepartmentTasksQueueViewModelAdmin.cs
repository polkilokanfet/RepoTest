using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public class EngineeringDepartmentTasksQueueViewModelAdmin : EngineeringDepartmentTasksQueueViewModel
    {
        public EngineeringDepartmentTasksQueueViewModelAdmin(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<EngineeringDepartmentTask> GetAllItems()
        {
            return UnitOfWork.Repository<PriceEngineeringTask>().Find(x =>
                    x.ParentPriceEngineeringTaskId == null &&
                    x.IsTotalAccepted == false &&
                    x.IsTotalStopped == false)
                .Select(x => new EngineeringDepartmentTaskPrice(x))
                .OrderBy(x => x);
        }
    }
}