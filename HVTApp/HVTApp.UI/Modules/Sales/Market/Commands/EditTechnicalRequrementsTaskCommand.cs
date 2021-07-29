using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class EditTechnicalRequrementsTaskCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IRegionManager _regionManager;

        public EditTechnicalRequrementsTaskCommand(Market2ViewModel viewModel, IRegionManager regionManager)
        {
            _viewModel = viewModel;
            _regionManager = regionManager;
        }

        protected override void ExecuteMethod()
        {
            _regionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(
                new NavigationParameters
                {
                    {
                        nameof(TechnicalRequrementsTask), _viewModel.TechnicalRequrementsTasks.SelectedItem.Entity
                    }
                });
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.TechnicalRequrementsTasks?.SelectedItem != null;
        }
    }
}