using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProjectViewModel : ViewModelBaseCanExportToExcel
    {
        public ProjectWrapper1 ProjectWrapper { get; }

        public ProjectTypes ProjectTypes { get; private set; }

        private IProjectUnit _selectedUnit;
        public IProjectUnit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                if (_selectedUnit == value) return;
                _selectedUnit = value;
                SelectedUnitChanged?.Invoke();
                RaisePropertyChanged();
            }
        }

        public event Action SelectedUnitChanged;

        public RoundUpModule RoundUpModule { get; } = new RoundUpModule();

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public ICommand RoundUpCommand { get; }

        public ICommand SaveCommand { get; }


        /// <summary>
        /// ��������� ������������ � ����� ������
        /// </summary>
        public MoveToNewProjectCommand MoveToNewProjectCommand { get; }

        /// <summary>
        /// ��������� ������������ � ������������ ������
        /// </summary>
        public MoveToExistsProjectCommand MoveToExistsProjectCommand { get; }

        /// <summary>
        /// �������� ������ � ������������
        /// </summary>
        public ICommand IncludeServiceInSpecificationCommand { get; }

        #endregion

        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="container"></param>
        public ProjectViewModel(IUnityContainer container) : this(new Project
        {
            ProjectTypeId = GlobalAppProperties.Actual.DefaultProjectType.Id
        }, container)
        {
            ProjectWrapper.Manager = new UserEmptyWrapper(this.UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
        }

        /// <summary>
        /// �������������� �������
        /// </summary>
        /// <param name="project"></param>
        /// <param name="container"></param>
        public ProjectViewModel(Project project, IUnityContainer container) : base(container)
        {
            project = ((IProjectRepository)(UnitOfWork.Repository<Project>())).GetForEdit(project.Id) ?? project;
            ProjectWrapper = new ProjectWrapper1(project);
            ProjectTypes = new ProjectTypes(UnitOfWork, this.ProjectWrapper);

            var selectService = container.Resolve<ISelectService>();
            var dialogService = container.Resolve<IDialogService>();
            var getProductService = container.Resolve<IGetProductService>();
            var eventAggregator = container.Resolve<IEventAggregator>();
            var messageService = container.Resolve<IMessageService>();

            EditCommand = new EditProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);
            AddCommand = new AddProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);
            RemoveCommand = new RemoveProjectUnitCommand(this, messageService, UnitOfWork);

            MoveToExistsProjectCommand = new MoveToExistsProjectCommand(this, UnitOfWork, container);
            MoveToNewProjectCommand = new MoveToNewProjectCommand(this, container);

            IncludeServiceInSpecificationCommand = new IncludeServiceInSpecificationCommand(this, container);

            SaveCommand = new SaveProjectCommand(this.ProjectWrapper, UnitOfWork, eventAggregator);

            RoundUpCommand = new DelegateCommand(() =>
            {
                foreach (var projectUnit in this.ProjectWrapper.Units)
                {
                    projectUnit.Cost = RoundUpModule.RoundUp(projectUnit.Cost);
                    if (projectUnit.CostWithReserve.HasValue)
                        projectUnit.CostWithReserve = RoundUpModule.RoundUp(projectUnit.CostWithReserve.Value);
                }
            });
        }

        /// <summary>
        /// ��� �������� ������������ � ������������ ������
        /// </summary>
        /// <param name="project"></param>
        /// <param name="container"></param>
        /// <param name="salesUnits"></param>
        public ProjectViewModel(Project project, IUnityContainer container, IEnumerable<SalesUnit> salesUnits) : this(project, container)
        {
            foreach (var salesUnit in salesUnits.Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.Id)))
            {
                salesUnit.Project = this.ProjectWrapper.Model;
                this.ProjectWrapper.Units.Add(new ProjectUnit(salesUnit));
            }
        }

        /// <summary>
        /// ��� �������� ������������ � ����� ������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="salesUnits"></param>
        public ProjectViewModel(IUnityContainer container, IEnumerable<SalesUnit> salesUnits) : this(container)
        {
            foreach (var salesUnit in salesUnits.Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.Id)))
            {
                salesUnit.Project = this.ProjectWrapper.Model;
                this.ProjectWrapper.Units.Add(new ProjectUnit(salesUnit));
            }
        }
    }
}