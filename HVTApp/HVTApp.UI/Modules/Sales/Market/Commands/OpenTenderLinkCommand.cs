using System.Diagnostics;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class OpenTenderLinkCommand : DelegateCommandBase
    {
        private readonly Market2ViewModel _viewModel;

        public OpenTenderLinkCommand(Market2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override void Execute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.Tenders.SelectedItem?.Link))
            {
                Process.Start(_viewModel.Tenders.SelectedItem.Link);
            }
        }

        protected override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.Tenders?.SelectedItem?.Link);
        }
    }
}