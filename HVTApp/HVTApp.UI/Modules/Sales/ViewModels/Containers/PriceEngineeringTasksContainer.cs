using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class PriceEngineeringTasksContainer : BaseContainerFilt<PriceEngineeringTasks, PriceEngineeringTasksLookup, SelectedPriceEngineeringTasksChangedEvent, AfterSavePriceEngineeringTasksEvent, AfterRemovePriceEngineeringTasksEvent, Project, SelectedProjectChangedEvent>
    {
        public PriceEngineeringTasksContainer(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<PriceEngineeringTasksLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Repository<PriceEngineeringTasks>()
                .Find(x => x.UserManager.Id == GlobalAppProperties.User.Id)
                .Select(x => new PriceEngineeringTasksLookup(x));
        }

        protected override IEnumerable<PriceEngineeringTasksLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(x => x.Entity.ChildPriceEngineeringTasks.SelectMany(t => t.SalesUnits).Select(s => s.Project).Any(p => p.Id == project.Id))
                .OrderByDescending(x => x.StartMoment);
        }
    }
}