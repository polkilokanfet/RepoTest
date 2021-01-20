using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Prism.Commands;
using Prism.Regions;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper, ProjectDetailsViewModel, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {
        private bool _isNew;

        /// <summary>
        /// Перенести оборудование в новый проект
        /// </summary>
        public ICommand MoveToNewProjectCommand { get; }

        /// <summary>
        /// Перенести оборудование в существующий проект
        /// </summary>
        public ICommand MoveToExistsProjectCommand { get; }

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            MoveToNewProjectCommand = new DelegateCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены, что хотите перенести это оборудование в новый проект?", defaultYes: true) != MessageDialogResult.Yes)
                    {
                        return;
                    }

                    regionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters
                    {
                        { nameof(SalesUnit), this.GroupsViewModel.Groups.SelectedGroup.SalesUnits }
                    });
                },
                () => this.GroupsViewModel.Groups.SelectedGroup != null);

            MoveToExistsProjectCommand = new DelegateCommand(
                () =>
                {
                    var projects = UnitOfWork.Repository<Project>().Find(x => x.Manager.Id == GlobalAppProperties.User.Id);
                    Project project = Container.Resolve<ISelectService>().SelectItem(projects);
                    if (project != null)
                    {
                        regionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters
                        {
                            { nameof(Project), project },
                            { nameof(SalesUnit), this.GroupsViewModel.Groups.SelectedGroup.SalesUnits }
                        });
                    }
                },
                () => this.GroupsViewModel.Groups.SelectedGroup != null);
        }

        public override void Load(Project project, bool isNew, object parameter = null)
        {
            _isNew = isNew;
            base.Load(project, isNew, parameter);
            
            //назначаем менеджера
            if (DetailsViewModel.Item.Manager == null)
            {
                var user = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Manager = new UserWrapper(user);
            }

            //проверяем актуальность команд при смене группы
            this.GroupsViewModel.Groups.SelectedGroupChanged += 
                grp => 
                {
                    ((DelegateCommand)MoveToNewProjectCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)MoveToExistsProjectCommand).RaiseCanExecuteChanged();
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

            if (project == null)
            {
                //перенос в новый проект
                this.Load(new Project(), false);
            }
            else
            {
                //перенос в существующий проект
                this.Load(project, false);
            }

            //назначение проекта во всех юнитах
            foreach (var grp in this.GroupsViewModel.Groups)
            {
                grp.Project = this.DetailsViewModel.Item;
            }
        }

        protected override IEnumerable<SalesUnit> GetUnits(Project project, object parameter = null)
        {
            List<SalesUnit> result = new List<SalesUnit>();

            if (!_isNew)
            {
                result = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id && !x.IsRemoved);
            }

            if (_movedSalesUnits != null)
                result.AddRange(_movedSalesUnits);

            return result;
        }
    }
}