using HVTApp.Infrastructure.Services;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class PrintOfferCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IUnityContainer _container;

        public PrintOfferCommand(Market2ViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            IFileManagerService fileManagerService = _container.Resolve<IFileManagerService>();
            _container.Resolve<IPrintOfferService>().PrintOffer(_viewModel.Offers.SelectedItem.Id, fileManagerService.GetPath(_viewModel.Offers.SelectedItem.Entity));
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.Offers?.SelectedItem != null;
        }
    }
}