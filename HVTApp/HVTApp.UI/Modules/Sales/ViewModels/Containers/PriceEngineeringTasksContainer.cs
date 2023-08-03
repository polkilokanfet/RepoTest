using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class PriceEngineeringTasksContainer : BaseContainerViewModelWithFilterByProject<PriceEngineeringTasks, PriceEngineeringTasksLookup, AfterSavePriceEngineeringTasksEvent, AfterRemovePriceEngineeringTasksEvent, PriceEngineeringTasksViewManager>
    {
        public PriceEngineeringTasksContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
        {
        }

        protected override IEnumerable<PriceEngineeringTasksLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Repository<PriceEngineeringTasks>()
                .Find(tasks => tasks.UserManager.Id == GlobalAppProperties.User.Id)
                .Select(tasks => new PriceEngineeringTasksLookup(tasks));
        }

        protected override IEnumerable<PriceEngineeringTasksLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(lookup => lookup.Entity.ChildPriceEngineeringTasks.SelectMany(task => task.SalesUnits).Select(salesUnit => salesUnit.Project).Any(project1 => project1.Id == project.Id))
                .OrderByDescending(lookup => lookup.StartMoment);
        }
    }
}