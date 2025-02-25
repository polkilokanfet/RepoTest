using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper1, ProjectDetailsViewModel1, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {
        private bool _isNew;

        public IEnumerable<ProjectType> ProjectTypes { get; private set; }

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

        public ProjectViewModel(IUnityContainer container) : base(container)
        {
            MoveToNewProjectCommand = new MoveToNewProjectCommand(this, Container);
            MoveToExistsProjectCommand = new MoveToExistsProjectCommand(this, UnitOfWork, Container);
            IncludeServiceInSpecificationCommand = new IncludeServiceInSpecificationCommand(this, container);
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

            ProjectTypes = UnitOfWork.Repository<ProjectType>().GetAll();
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
            var result = new List<SalesUnit>();

            if (!_isNew)
            {
                result.AddRange(((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetByProject(project.Id).Where(salesUnit => !salesUnit.IsRemoved));
            }

            if (_movedSalesUnits != null)
                result.AddRange(_movedSalesUnits);

            return result;
        }
    }

    public class ProjectTypes : IEnumerable<ProductType>
    {
        private readonly ProjectWrapper1 _projectWrapper1;
        private readonly List<ProductType> _productTypes;
        private ProductType _selectedProductType;

        public ProductType SelectedProductType
        {
            get => _selectedProductType;
            set
            {
                if (Equals(_selectedProductType, value)) return;
                _selectedProductType = value;
                this._projectWrapper1.ProjectTypeId = value.Id;
            }
        }

        public ProjectTypes(IUnitOfWork unitOfWork, ProjectWrapper1 projectWrapper1)
        {
            _productTypes = unitOfWork.Repository<ProductType>().GetAllAsNoTracking();
            _projectWrapper1 = projectWrapper1;
            if (_projectWrapper1.ProjectTypeId != Guid.Empty)
                _selectedProductType = _productTypes.Single(x => x.Id == _projectWrapper1.ProjectTypeId);
        }

        public IEnumerator<ProductType> GetEnumerator()
        {
            return _productTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ProjectViewModel1
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectWrapper1 ProjectWrapper { get; }

        public ProjectTypes ProjectTypes { get; private set; }

        #region Commands

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }


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
        /// Создание нового проекта
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
        public ProjectViewModel1(Project project, IUnityContainer container)
        {
            _unitOfWork = container.Resolve<IUnitOfWork>();
            var selectService = container.Resolve<ISelectService>();
            var dialogService = container.Resolve<IDialogService>();

            EditCommand = new EditProjectUnitCommand(_unitOfWork, selectService, dialogService, this);
            AddCommand = new AddProjectUnitCommand(_unitOfWork, selectService, dialogService, this);

            MoveToNewProjectCommand = new MoveToNewProjectCommand(this, container);
            MoveToExistsProjectCommand = new MoveToExistsProjectCommand(this, _unitOfWork, container);
            IncludeServiceInSpecificationCommand = new IncludeServiceInSpecificationCommand(this, container);

            ProjectWrapper = new ProjectWrapper1(project);
            ProjectTypes = new ProjectTypes(_unitOfWork, this.ProjectWrapper);
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
            var result = new List<SalesUnit>();

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