using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class TechnicalRequrementsTasksContainer : BaseContainerViewModelWithFilterByProject<TechnicalRequrementsTask, TechnicalRequrementsTaskLookup, SelectedTechnicalRequrementsTaskChangedEvent, AfterSaveTechnicalRequrementsTaskEvent, AfterRemoveTechnicalRequrementsTaskEvent, TechnicalRequrementsTaskView>
    {
        public TechnicalRequrementsTasksContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
        {
        }

        protected override IEnumerable<TechnicalRequrementsTaskLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            return GlobalAppProperties.User.RoleCurrent == Role.Admin 
                    ? unitOfWork.Repository<TechnicalRequrementsTask>().GetAll().Select(x => new TechnicalRequrementsTaskLookup(x))
                    : unitOfWork.Repository<TechnicalRequrementsTask>()
                        .Find(technicalRequrementsTask => technicalRequrementsTask.Requrements.SelectMany(technicalRequrements => technicalRequrements.SalesUnits).Any(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser()))
                        .Select(technicalRequrementsTask => new TechnicalRequrementsTaskLookup(technicalRequrementsTask));
        }

        protected override IEnumerable<TechnicalRequrementsTaskLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(technicalRequrementsTaskLookup => technicalRequrementsTaskLookup.Requrements.SelectMany(technicalRequrementsLookup => technicalRequrementsLookup.SalesUnits).Any(su => su.Project.Id == project.Id))
                .OrderByDescending(technicalRequrementsTaskLookup => technicalRequrementsTaskLookup.Start);
        }

        protected override bool CanBeShown(TechnicalRequrementsTask technicalRequrementsTask)
        {
            return this.SelectedProject != null && 
                   technicalRequrementsTask.Requrements
                       .SelectMany(technicalRequrements => technicalRequrements.SalesUnits)
                       .Any(salesUnit => salesUnit.Project.Id == this.SelectedProject.Id);
        }
    }
}