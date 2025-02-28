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
        public ProjectViewModel1(Project project, IUnityContainer container) : base(container)
        {
            var selectService = container.Resolve<ISelectService>();
            var dialogService = container.Resolve<IDialogService>();
            var getProductService = container.Resolve<IGetProductService>();

            EditCommand = new EditProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);
            AddCommand = new AddProjectUnitCommand(UnitOfWork, selectService, dialogService, this, getProductService);

            //MoveToNewProjectCommand = new MoveToNewProjectCommand(this, container);
            //MoveToExistsProjectCommand = new MoveToExistsProjectCommand(this, _unitOfWork, container);
            //IncludeServiceInSpecificationCommand = new IncludeServiceInSpecificationCommand(this, container);

            ProjectWrapper = new ProjectWrapper1(UnitOfWork.Repository<Project>().GetById(project.Id));
            ProjectTypes = new ProjectTypes(UnitOfWork, this.ProjectWrapper);
        }

        ////переносимые юниты
        //private List<SalesUnit> _movedSalesUnits;

        ////загрузка при переносе SalesUnits в другой проект
        //public void LoadForMove(List<SalesUnit> salesUnits, Project project = null)
        //{
        //    //переносимые юниты
        //    _movedSalesUnits = new List<SalesUnit>();
        //    foreach (var unit in salesUnits)
        //    {
        //        _movedSalesUnits.Add(UnitOfWork.Repository<SalesUnit>().GetById(unit.Id));
        //    }

        //    this.Load(project ?? new Project(), false);

        //    //назначение проекта во всех юнитах
        //    ProjectSimpleWrapper projectSimpleWrapper = new ProjectSimpleWrapper(this.DetailsViewModel.Item.Model);
        //    foreach (var projectUnitsGroup in this.GroupsViewModel.Groups)
        //    {
        //        if (projectUnitsGroup.Project.Model.Id != projectSimpleWrapper.Model.Id)
        //        {
        //            projectUnitsGroup.Project = projectSimpleWrapper;
        //        }
        //    }
        //}

        //protected override IEnumerable<SalesUnit> GetUnits(Project project, object parameter = null)
        //{
        //    var result = new List<SalesUnit>();

        //    if (!_isNew)
        //    {
        //        result.AddRange(((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetByProject(project.Id).Where(salesUnit => !salesUnit.IsRemoved));
        //    }

        //    if (_movedSalesUnits != null)
        //        result.AddRange(_movedSalesUnits);

        //    return result;
        //}
    }
}