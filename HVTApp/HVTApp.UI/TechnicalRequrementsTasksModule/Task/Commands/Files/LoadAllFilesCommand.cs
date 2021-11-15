using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class LoadAllFilesCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        private readonly IFileManagerService _fileManagerService;
        public LoadAllFilesCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
            _fileManagerService = container.Resolve<IFileManagerService>();
        }

        protected override void ExecuteMethod()
        {
            using (var fdb = new FolderBrowserDialog())
            {
                var result = fdb.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                {
                    var taskPath = fdb.SelectedPath;
                    foreach (var requrement in ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Where(x => x.IsActual.HasValue && x.IsActual.Value))
                    {
                        var reqDirName = $"{requrement.Model.Id} {requrement.SalesUnit.Product.Designation.ReplaceUncorrectSimbols().LimitLengh()} ({requrement.Amount} רע.)";
                        var dirPath = Path.Combine(taskPath, reqDirName);
                        _fileManagerService.CreateDirectoryPathIfNotExists(dirPath);

                        foreach (var file in requrement.Files.Where(technicalRequrementsFileWrapper => technicalRequrementsFileWrapper.IsActual))
                        {
                            var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
                            string addToFileName = $"{file.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                            FilesStorageService.CopyFileFromStorage(file.Id, storageDirectory, dirPath, addToFileName, false);
                        }
                    }

                    Process.Start(taskPath);
                }
            }

        }
    }
}