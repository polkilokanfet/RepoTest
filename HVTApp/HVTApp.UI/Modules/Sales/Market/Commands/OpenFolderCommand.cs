using System.Diagnostics;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class OpenFolderCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;

        public OpenFolderCommand(Market2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override void ExecuteMethod()
        {
            var path = PathGetter.GetPath(_viewModel.SelectedProjectItem.Project);
            Process.Start("explorer", $"\"{path}\"");
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}