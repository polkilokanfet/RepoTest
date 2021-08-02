using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel
{
    public class MoveToNewProjectCommand : DelegateLogCommand
    {
        private readonly ProjectViewModel _viewModel;
        private readonly IUnityContainer _container;

        public MoveToNewProjectCommand(ProjectViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }
        protected override void ExecuteMethod()
        {
            if (_container.Resolve<IMessageService>().ShowYesNoMessageDialog("ѕеремещение", "¬ы уверены, что хотите перенести это оборудование в новый проект?", defaultYes: true) != MessageDialogResult.Yes)
            {
                return;
            }

            _container.Resolve<IRegionManager>().RequestNavigateContentRegion<ProjectView>(new NavigationParameters
            {
                { nameof(SalesUnit), _viewModel.GroupsViewModel.Groups.SelectedGroup.SalesUnits }
            });
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.GroupsViewModel.Groups.SelectedGroup != null;
        }
    }
}