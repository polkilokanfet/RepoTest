using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class ProjectsContainer : BaseContainer<Project, ProjectLookup, SelectedProjectChangedEvent, AfterSaveProjectEvent, AfterRemoveProjectEvent>
    {
        private List<SalesUnit> _salesUnits;
        private List<Tender> _tenders;

        public ProjectsContainer(IUnityContainer container) : base(container)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();

            //реакция на сохранение строки пректа
            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit => { _salesUnits.ReAddById(salesUnit); });

            //реакция на сохранение тендера
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender => { _tenders.ReAddById(tender); });

            this.AddRange(AllLookups);
        }

        protected override IEnumerable<ProjectLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            _tenders = unitOfWork.Repository<Tender>().Find(x => x.Project.Manager.IsAppCurrentUser());
            _salesUnits = unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
            return _salesUnits.Select(x => x.Project).Distinct().Select(MakeLookup);
        }

        protected override ProjectLookup MakeLookup(Project project)
        {
            var units = _salesUnits.Where(su => su.Project.Id == project.Id);
            var tenders = _tenders.Where(su => su.Project.Id == project.Id);
            return new ProjectLookup(project, units, tenders, Container);
        }

        /// <summary>
        /// Проект является рабочим
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private bool IsWork(Project project)
        {
            return project.InWork && _salesUnits.Where(x => x.Project.Id == project.Id).Any(u => !u.IsDone && !u.IsLoosen);
        }

        public override async Task RemoveSelectedItemTask()
        {
            var project = SelectedItem.Entity;

            await base.RemoveSelectedItemTask();

            var salesUnits = _salesUnits.Where(x => x.Project == null || x.Project.Id == project.Id).ToList();
            salesUnits.ForEach(x => Container.Resolve<IEventAggregator>().GetEvent<AfterRemoveSalesUnitEvent>().Publish(x));
        }
    }
}