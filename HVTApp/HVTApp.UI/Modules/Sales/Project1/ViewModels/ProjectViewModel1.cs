using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProjectViewModel1 : ViewModelBaseCanExportToExcel
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
                RaisePropertyChanged();
            }
        }

        public RoundUpModule RoundUpModule { get; } = new RoundUpModule();

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public ICommand RoundUpCommand { get; }

        public ICommand SaveCommand { get; }


        /// <summary>
        /// Перенести оборудование в новый проект
        /// </summary>
        public MoveToNewProjectCommand MoveToNewProjectCommand { get; }

        /// <summary>
        /// Перенести оборудование в существующий проект
        /// </summary>
        public MoveToExistsProjectCommand MoveToExistsProjectCommand { get; }

        /// <summary>
        /// Включить услугу в спецификацию
        /// </summary>
        public ICommand IncludeServiceInSpecificationCommand { get; }

        #endregion

        /// <summary>
        /// Создание проекта
        /// </summary>
        /// <param name="container"></param>
        public ProjectViewModel1(IUnityContainer container) : this(new Project
        {
            ProjectTypeId = GlobalAppProperties.Actual.DefaultProjectType.Id,
            ManagerId = GlobalAppProperties.User.Id
        }, container)
        {
        }

        /// <summary>
        /// Редактирование проекта
        /// </summary>
        /// <param name="project"></param>
        /// <param name="container"></param>
        public ProjectViewModel1(Project project, IUnityContainer container) : base(container)
        {
            project = UnitOfWork.Repository<Project>().GetById(project.Id) ?? project;
            ProjectWrapper = new ProjectWrapper1(project);
            ProjectTypes = new ProjectTypes(UnitOfWork, this.ProjectWrapper);

            var selectService = container.Resolve<ISelectService>();
            var dialogService = container.Resolve<IDialogService>();
            var getProductService = container.Resolve<IGetProductService>();
            var eventAggregator = container.Resolve<IEventAggregator>();

            EditCommand = new EditProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);
            AddCommand = new AddProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);

            SaveCommand = new SaveProjectCommand(this.ProjectWrapper, UnitOfWork, eventAggregator);

            RoundUpCommand = new DelegateCommand(() => this.ProjectWrapper.Units.ForEach(projectUnit => projectUnit.Cost = RoundUpModule.RoundUp(projectUnit.Cost)));
        }
    }
}