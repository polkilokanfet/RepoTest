using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using HVTApp.DataAccess;
using Prism.Commands;
using Prism.Regions;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper1, ProjectDetailsViewModel1, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {
        private bool _isNew;

        /// <summary>
        /// ��������� ������������ � ����� ������
        /// </summary>
        public DelegateLogCommand MoveToNewProjectCommand { get; }

        /// <summary>
        /// ��������� ������������ � ������������ ������
        /// </summary>
        public DelegateLogCommand MoveToExistsProjectCommand { get; }

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
            var regionManager = Container.Resolve<IRegionManager>();
            MoveToNewProjectCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "�� �������, ��� ������ ��������� ��� ������������ � ����� ������?", defaultYes: true) != MessageDialogResult.Yes)
                    {
                        return;
                    }

                    regionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters
                    {
                        { nameof(SalesUnit), this.GroupsViewModel.Groups.SelectedGroup.SalesUnits }
                    });
                },
                () => this.GroupsViewModel.Groups.SelectedGroup != null);

            MoveToExistsProjectCommand = new DelegateLogCommand(
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
            
            //��������� ���������
            if (DetailsViewModel.Item.Model.Manager == null)
            {
                var manager = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
                DetailsViewModel.Item.Model.Manager = manager;
            }

            if (isNew)
            {
                //��������� ����������� ��� �������
                if (GlobalAppProperties.Actual.DefaultProjectType != null)
                {
                    ProjectType defaultProjectType = UnitOfWork.Repository<ProjectType>().GetById(GlobalAppProperties.Actual.DefaultProjectType.Id);
                    DetailsViewModel.Item.ProjectType = new ProjectTypeSimpleWrapper(defaultProjectType);
                }
            }

            //��������� ������������ ������ ��� ����� ������
            this.GroupsViewModel.Groups.SelectedGroupChanged += 
                grp => 
                {
                    MoveToNewProjectCommand.RaiseCanExecuteChanged();
                    MoveToExistsProjectCommand.RaiseCanExecuteChanged();
                };
        }

        //����������� �����
        private List<SalesUnit> _movedSalesUnits;

        //�������� ��� �������� SalesUnits � ������ ������
        public void LoadForMove(List<SalesUnit> salesUnits, Project project = null)
        {
            //����������� �����
            _movedSalesUnits = new List<SalesUnit>();
            foreach (var unit in salesUnits)
            {
                _movedSalesUnits.Add(UnitOfWork.Repository<SalesUnit>().GetById(unit.Id));
            }

            this.Load(project ?? new Project(), false);

            //���������� ������� �� ���� ������
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