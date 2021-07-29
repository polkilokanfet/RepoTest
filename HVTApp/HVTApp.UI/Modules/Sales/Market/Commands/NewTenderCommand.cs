using System.ComponentModel;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class NewTenderCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnityContainer _container;

        public NewTenderCommand(Market2ViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            var tenderViewModel = new TenderViewModel(_container, _viewModel.SelectedProjectItem.Project);
            _container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}