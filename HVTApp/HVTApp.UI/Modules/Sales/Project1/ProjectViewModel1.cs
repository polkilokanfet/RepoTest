using System;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1
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