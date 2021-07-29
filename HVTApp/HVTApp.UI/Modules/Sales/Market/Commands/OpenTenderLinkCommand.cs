using System.Diagnostics;
using HVTApp.UI.Commands;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class OpenTenderLinkCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;

        public OpenTenderLinkCommand(Market2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override void ExecuteMethod()
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.Tenders.SelectedItem?.Link))
            {
                Process.Start(_viewModel.Tenders.SelectedItem.Link);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Tenders?.SelectedItem?.Link);
        }
    }
}