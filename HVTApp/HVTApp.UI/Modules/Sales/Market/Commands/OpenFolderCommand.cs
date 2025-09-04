using System.Diagnostics;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class OpenFolderCommand : DelegateLogCommand
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IFileManagerService _fileManagerService;


        public OpenFolderCommand(Market2ViewModel viewModel, IFileManagerService fileManagerService)
        {
            _viewModel = viewModel;
            _fileManagerService = fileManagerService;
        }

        protected override void ExecuteMethod()
        {
            var path = _fileManagerService.GetPath(_viewModel.SelectedProjectItem.Project.Id);
            Process.Start($"\"{path}\"");
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedProjectItem != null;
        }
    }
}