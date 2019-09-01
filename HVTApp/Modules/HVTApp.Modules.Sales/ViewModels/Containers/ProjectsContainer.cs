using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsContainer : BaseContainer<Project, ProjectLookup, SelectedProjectChangedEvent, AfterSaveProjectEvent, AfterRemoveProjectEvent>
    {
        private bool _shownAllProjects = true;
        private List<SalesUnit> _salesUnits;
        private List<Tender> _tenders;

        public ProjectsContainer(IUnityContainer container) : base(container)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit => _salesUnits.ReAddById(salesUnit));
            eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender => _tenders.ReAddById(tender));

            ShownAllProjects = false;
        }

        protected override IEnumerable<Project> GetItems(IUnitOfWorkDisplay unitOfWork)
        {
            _tenders = unitOfWork.Repository<Tender>().Find(x => x.Project.Manager.IsAppCurrentUser());
            _salesUnits = unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
            return _salesUnits.Select(x => x.Project).Distinct();
        }

        private ProjectLookup GetLookup(Project project)
        {
            var units = _salesUnits.Where(su => su.Project.Id == project.Id);
            var tenders = _tenders.Where(su => su.Project.Id == project.Id);
            return new ProjectLookup(project, units, tenders, Container);
        }

        public bool ShownAllProjects
        {
            get { return _shownAllProjects; }
            set
            {
                if(Equals(_shownAllProjects, value))
                    return;
                _shownAllProjects = value;

                this.Clear();
                var projects = ShownAllProjects ? AllItems : AllItems.Where(IsWork);
                this.AddRange(projects.Select(GetLookup).OrderBy(x => x.RealizationDate));
            }
        }

        /// <summary>
        /// Проект является рабочим
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private bool IsWork(Project project)
        {
            return project.InWork && _salesUnits.Any(u => !u.IsDone && !u.IsLoosen);
        }
    }
}