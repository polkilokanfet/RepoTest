using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectViewModel : UnitsContainer<Project, ProjectWrapper1, ProjectDetailsViewModel1, SalesUnitsGroupsViewModel, SalesUnit, AfterSaveProjectEvent>
    {
        private bool _isNew;

        public IEnumerable<ProjectType> ProjectTypes { get; private set; }

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

            ProjectTypes = UnitOfWork.Repository<ProjectType>().GetAll();
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