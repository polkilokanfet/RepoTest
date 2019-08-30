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

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsContainer : BaseContainer<Project, ProjectLookup, SelectedProjectChangedEvent, AfterSaveProjectEvent, AfterRemoveProjectEvent>
    {
        private bool _shownAllProjects;
        private List<SalesUnit> _salesUnits;

        public ProjectsContainer(IUnityContainer container) : base(container)
        {
            ShowWorkProjects();
        }

        protected override List<Project> GetItems(IUnitOfWork unitOfWork)
        {
            _salesUnits = unitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.IsAppCurrentUser());
            return _salesUnits.Select(x => x.Project).Distinct().ToList();
        }

        public bool ShownAllProjects
        {
            get { return _shownAllProjects; }
            set
            {
                if(Equals(_shownAllProjects, value))
                    return;
                _shownAllProjects = value;

                if (ShownAllProjects)
                {
                    ShowAllProjects();
                }
                else
                {
                    ShowWorkProjects();
                }
            }
        }

        /// <summary>
        /// Показать все проекты
        /// </summary>
        private void ShowAllProjects()
        {
            Show(AllItems.Select(x => new ProjectLookup(x)));
            ShownAllProjects = true;
        }

        /// <summary>
        /// Показать рабочие проекты
        /// </summary>
        private void ShowWorkProjects()
        {
            //var projects = AllItems.Where(IsWork);
            //Show(projects);
            //ShownAllProjects = false;
        }

        private void Show(IEnumerable<ProjectLookup> projects)
        {
            this.Clear();
            this.AddRange(projects.OrderBy(x => x.RealizationDate));
        }

        /// <summary>
        /// Проект является рабочим
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private static bool IsWork(ProjectLookup project)
        {
            return project.Entity.InWork && project.SalesUnits.Any(u => !u.IsDone && !u.IsLoosen);
        }
    }
}