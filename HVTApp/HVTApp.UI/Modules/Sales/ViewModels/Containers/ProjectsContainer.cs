using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Project1;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class ProjectsContainer : BaseContainerViewModel<Project, ProjectLookup, AfterSaveProjectEvent, AfterRemoveProjectEvent, ProjectView>
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
            return _salesUnits.Select(salesUnit => salesUnit.Project).Distinct().Select(MakeLookup);
        }

        protected override ProjectLookup MakeLookup(Project project)
        {
            var units = _salesUnits.Where(salesUnit => salesUnit.Project.Id == project.Id);
            var tenders = _tenders.Where(tender => tender.Project.Id == project.Id);
            return new ProjectLookup(project, units, tenders, Container);
        }

        /// <summary>
        /// Проект является рабочим
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private bool IsWork(Project project)
        {
            return project.InWork && 
                   _salesUnits
                       .Where(salesUnit => salesUnit.Project.Id == project.Id)
                       .Any(salesUnit => !salesUnit.IsDone && !salesUnit.IsLoosen);
        }

        public override void RemoveSelectedItem()
        {
            var project = SelectedItem.Entity;

            base.RemoveSelectedItem();

            var salesUnits = _salesUnits.Where(salesUnit => salesUnit.Project == null || salesUnit.Project.Id == project.Id).ToList();
            salesUnits.ForEach(salesUnit => Container.Resolve<IEventAggregator>().GetEvent<AfterRemoveSalesUnitEvent>().Publish(salesUnit));
        }
    }
}