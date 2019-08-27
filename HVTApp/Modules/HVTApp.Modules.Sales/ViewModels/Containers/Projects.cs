using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Projects : List<ProjectLookup>
    {
        public bool ShownAllProjects { get; private set; } = false;

        public ObservableCollection<ProjectLookup> ProjectsToShow { get; } = new ObservableCollection<ProjectLookup>();

        public new void Add(ProjectLookup project)
        {
            base.Add(project);
            if (IsWork(project) || ShownAllProjects)
            {
                ProjectsToShow.Add(project);
            }
        }

        public new void Remove(ProjectLookup project)
        {
            base.Remove(project);
            ProjectsToShow.Remove(project);
        }

        /// <summary>
        /// Показать все проекты
        /// </summary>
        public void ShowAll()
        {
            if(ShownAllProjects) return;
            Show(this);
            ShownAllProjects = true;
        }

        /// <summary>
        /// Показать рабочие проекты
        /// </summary>
        public void ShowWork()
        {
            if(!ShownAllProjects) return;
            var projects = this.Where(IsWork);
            Show(projects);
            ShownAllProjects = false;
        }

        private void Show(IEnumerable<ProjectLookup> projects)
        {
            ProjectsToShow.Clear();
            ProjectsToShow.AddRange(projects.OrderBy(x => x.RealizationDate));
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