using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using HVTApp.UI.Modules.Sales.Project1.Views;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class MoveToExistsProjectCommand : RaiseCanExecuteChangedCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnityContainer _container;

        public MoveToExistsProjectCommand(ProjectViewModel viewModel, IUnitOfWork unitOfWork, IUnityContainer container)
        {
            _viewModel = viewModel;
            _unitOfWork = unitOfWork;
            _container = container;

            _viewModel.SelectedUnitChanged += RaiseCanExecuteChanged;
        }

        public override void Execute(object parameter)
        {
            var projects = _unitOfWork.Repository<Project>().Find(project1 => project1.Manager.Id == GlobalAppProperties.User.Id);
            var project = _container.Resolve<ISelectService>().SelectItem(projects);
            if (project == null) return;

            IEnumerable<SalesUnit> salesUnits = _viewModel.SelectedUnit is ProjectUnitGroup projectUnitGroup
                ? projectUnitGroup.Units.Select(x => x.Model)
                : new[] { ((ProjectUnit)_viewModel.SelectedUnit).Model };

            _container.Resolve<IRegionManager>().RequestNavigateContentRegion<ProjectView>(new NavigationParameters
            {
                { nameof(Project), project },
                { nameof(SalesUnit), salesUnits }
            });
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedUnit != null;
        }
    }
}