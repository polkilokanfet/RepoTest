using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class MoveToExistsProjectCommand : DelegateLogCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnityContainer _container;

        public MoveToExistsProjectCommand(ProjectViewModel viewModel, IUnitOfWork unitOfWork, IUnityContainer container)
        {
            _viewModel = viewModel;
            _unitOfWork = unitOfWork;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            var projects = _unitOfWork.Repository<Project>().Find(project1 => project1.Manager.Id == GlobalAppProperties.User.Id);
            Project project = _container.Resolve<ISelectService>().SelectItem(projects);
            if (project != null)
            {
                _container.Resolve<IRegionManager>().RequestNavigateContentRegion<ProjectView>(new NavigationParameters
                {
                    { nameof(Project), project },
                    { nameof(SalesUnit), _viewModel.GroupsViewModel.Groups.SelectedGroup.SalesUnits }
                });
            }
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.GroupsViewModel.Groups.SelectedGroup != null;
        }
    }
}