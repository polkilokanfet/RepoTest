using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel
    {

        #region AfterSaveEvents

        //private void AfterSaveProjectEventExecute(Project project)
        //{
        //    var projectLookup = Projects.GetById(project);

        //    //если необходимо обновить существующий проект
        //    if (projectLookup != null)
        //    {
        //        projectLookup.Refresh(project);

        //        if (ShownAllProjects || projectLookup.InWork)
        //        {
        //            LoadGroups(projectLookup);
        //        }
        //        else
        //        {
        //            //удяляем нерабочие проекты
        //            if (Projects.ProjectsToShow.Contains(projectLookup))
        //            {
        //                Projects.ProjectsToShow.Remove(projectLookup);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        var lookup = new ProjectLookup(project);
        //        Projects.Add(lookup);

        //        if (ShownAllProjects || lookup.InWork)
        //            Projects.ProjectsToShow.Add(lookup);
        //    }
        //}

        private void AfterSaveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            //целевой проект
            var project = Projects.GetById(salesUnit.Project);

            //костыль - нужно добавить предварительно проект
            if (project == null) return;

            //обновляем или добавляем
            if (project.SalesUnits.ContainsById(salesUnit))
            {
                project.SalesUnits.GetById(salesUnit).Refresh(salesUnit);
            }
            else
            {
                project.SalesUnits.Add(new SalesUnitLookup(salesUnit));
            }

            //обновляем целевой проект
            project.Refresh();

            //обновляем отображение оборудования
            if (Equals(project, SelectedProjectLookup))
            {
                LoadGroups(SelectedProjectLookup);
            }
        }

        #endregion

        #region AfterRemoveEvent

        private void AfterRemoveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            var project = Projects.GetById(salesUnit.Project);
            if (project == null) return;
            var lookup = project.SalesUnits.GetById(salesUnit);
            project.SalesUnits.Remove(lookup);
            project.Refresh();
        }

        private void AfterRemoveProjectEventExecute(Project project)
        {
        }

        #endregion

    }
}
