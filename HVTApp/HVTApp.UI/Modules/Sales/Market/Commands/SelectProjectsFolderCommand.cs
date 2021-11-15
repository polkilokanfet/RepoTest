using System.Windows.Forms;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class SelectProjectsFolderCommand : DelegateLogCommand
    {
        private readonly IFileManagerService _fileManagerService;

        public SelectProjectsFolderCommand(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }

        protected override void ExecuteMethod()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _fileManagerService.SaveDefaultProjectsFolderPath(dialog.SelectedPath);
                }
            }
        }
    }
}