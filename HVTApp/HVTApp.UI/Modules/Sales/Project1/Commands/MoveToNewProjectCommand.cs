using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Views;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class MoveToNewProjectCommand : RaiseCanExecuteChangedCommand
    {
        private readonly ProjectViewModel1 _viewModel;
        private readonly IUnityContainer _container;

        public MoveToNewProjectCommand(ProjectViewModel1 viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;

            _viewModel.SelectedUnitChanged += RaiseCanExecuteChanged;
        }

        public override void Execute(object parameter)
        {
            IEnumerable<SalesUnit> salesUnits = _viewModel.SelectedUnit is ProjectUnitGroup projectUnitGroup
                ? projectUnitGroup.Units.Select(x => x.Model)
                : new[] { ((ProjectUnit)_viewModel.SelectedUnit).Model };

            _container.Resolve<IRegionManager>().RequestNavigateContentRegion<ProjectView1>(new NavigationParameters
            {
                { nameof(SalesUnit), salesUnits }
            });
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedUnit != null;
        }
    }
}