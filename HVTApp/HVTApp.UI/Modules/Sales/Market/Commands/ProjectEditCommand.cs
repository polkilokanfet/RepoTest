using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Views;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class ProjectEditCommand : DelegateCommandBase
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public ProjectEditCommand(Market2ViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void Execute(object parameter)
        {
            _regionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters
            {
                {
                    nameof(Project), _viewModel.SelectedProjectItem.Project
                }
            });
        }

        protected override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}