using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class SaveProjectCommand : ICommand
    {
        private readonly ProjectWrapper1 _projectWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private bool _canExecuteFlag;

        private bool CanExecuteFlag
        {
            set
            {
                if (_canExecuteFlag == value) return;
                _canExecuteFlag = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public SaveProjectCommand(ProjectWrapper1 projectWrapper, IUnitOfWork unitOfWork)
        {
            _projectWrapper = projectWrapper;
            _unitOfWork = unitOfWork;
            _projectWrapper.PropertyChanged += (sender, args) =>
            {
                CanExecuteFlag = CanExecute(null);
            };
        }

        public bool CanExecute(object parameter)
        {
            return _projectWrapper.IsValid &&
                   _projectWrapper.IsChanged;
        }

        public void Execute(object parameter)
        {
            _projectWrapper.AcceptChanges();
            _unitOfWork.SaveEntity(_projectWrapper.Model);
            CanExecuteFlag = CanExecute(null);
        }

        public event EventHandler CanExecuteChanged;
    }

    public class ProjectViewModel1 : ViewModelBaseCanExportToExcel
    {
        public ProjectWrapper1 ProjectWrapper { get; }

        public ProjectTypes ProjectTypes { get; private set; }

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

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
            ProjectWrapper = new ProjectWrapper1(UnitOfWork.Repository<Project>().GetById(project.Id));
            ProjectTypes = new ProjectTypes(UnitOfWork, this.ProjectWrapper);

            var selectService = container.Resolve<ISelectService>();
            var dialogService = container.Resolve<IDialogService>();
            var getProductService = container.Resolve<IGetProductService>();

            EditCommand = new EditProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);
            AddCommand = new AddProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);

            SaveCommand = new SaveProjectCommand(this.ProjectWrapper, UnitOfWork);
        }
    }
}