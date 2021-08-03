using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.Unity;
using HVTApp.DataAccess;
using Prism.Regions;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper1, ProjectDetailsViewModel1, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {
        private bool _isNew;

        /// <summary>
        /// Перенести оборудование в новый проект
        /// </summary>
        public MoveToNewProjectCommand MoveToNewProjectCommand { get; }

        /// <summary>
        /// Перенести оборудование в существующий проект
        /// </summary>
        public MoveToExistsProjectCommand MoveToExistsProjectCommand { get; }

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            MoveToNewProjectCommand = new MoveToNewProjectCommand(this, Container);
            MoveToExistsProjectCommand = new MoveToExistsProjectCommand(this, UnitOfWork, Container);
        }

        public override void Load(Project project, bool isNew, object parameter = null)
        {
            _isNew = isNew;
            base.Load(project, isNew, parameter);
            
            //назначаем менеджера
            if (DetailsViewModel.Item.Model.Manager == null)
            {
                var manager = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Model.Manager = manager;
            }

            if (isNew)
            {
                //назначаем стандартный тип проекта
                if (GlobalAppProperties.Actual.DefaultProjectType != null)
                {
                    ProjectType defaultProjectType = UnitOfWork.Repository<ProjectType>().GetById(GlobalAppProperties.Actual.DefaultProjectType.Id);
                    DetailsViewModel.Item.ProjectType = new ProjectTypeSimpleWrapper(defaultProjectType);
                }
            }

            //проверяем актуальность команд при смене группы
            this.GroupsViewModel.Groups.SelectedGroupChanged += 
                grp => 
                {
                    MoveToNewProjectCommand.RaiseCanExecuteChanged();
                    MoveToExistsProjectCommand.RaiseCanExecuteChanged();
                };
        }

        //переносимые юниты
        private List<SalesUnit> _movedSalesUnits;

        //загрузка при переносе SalesUnits в другой проект
        public void LoadForMove(List<SalesUnit> salesUnits, Project project = null)
        {
            //переносимые юниты
            _movedSalesUnits = new List<SalesUnit>();
            foreach (var unit in salesUnits)
            {
                _movedSalesUnits.Add(UnitOfWork.Repository<SalesUnit>().GetById(unit.Id));
            }

            this.Load(project ?? new Project(), false);

            //назначение проекта во всех юнитах
            ProjectSimpleWrapper projectSimpleWrapper = new ProjectSimpleWrapper(this.DetailsViewModel.Item.Model);
            foreach (var projectUnitsGroup in this.GroupsViewModel.Groups)
            {
                if (projectUnitsGroup.Project.Model.Id != projectSimpleWrapper.Model.Id)
                {
                    projectUnitsGroup.Project = projectSimpleWrapper;
                }
            }
        }

        protected override IEnumerable<SalesUnit> GetUnits(Project project, object parameter = null)
        {
            List<SalesUnit> result = new List<SalesUnit>();

            if (!_isNew)
            {
                result.AddRange(((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetByProject(project.Id).Where(salesUnit => !salesUnit.IsRemoved));
            }

            if (_movedSalesUnits != null)
                result.AddRange(_movedSalesUnits);

            return result;
        }
    }
}