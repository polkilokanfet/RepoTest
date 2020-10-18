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
    public class TechnicalRequrementsTasksContainer : BaseContainerFilt<TechnicalRequrementsTask, TechnicalRequrementsTaskLookup, SelectedTechnicalRequrementsTaskChangedEvent, AfterSaveTechnicalRequrementsTaskEvent, AfterRemoveTechnicalRequrementsTaskEvent, Project, SelectedProjectChangedEvent>
    {
        public TechnicalRequrementsTasksContainer(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<TechnicalRequrementsTaskLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Repository<TechnicalRequrementsTask>()
                .Find(x => x.Requrements.SelectMany(r => r.SalesUnits).Any(su => su.Project.Manager.IsAppCurrentUser()))
                .Select(x => new TechnicalRequrementsTaskLookup(x));
        }

        protected override IEnumerable<TechnicalRequrementsTaskLookup> GetActualLookups(Project project)
        {
            return AllLookups.Where(x => x.Requrements.SelectMany(r => r.SalesUnits).Any(su => su.Project.Id == project.Id)).OrderBy(x => x.Start);
        }

        protected override bool CanBeShown(TechnicalRequrementsTask technicalRequrementsTask)
        {
            return Filter != null && technicalRequrementsTask.Requrements.SelectMany(x => x.SalesUnits).Any(su => su.Project.Id == Filter.Id);
        }
    }
}