using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class LoadAllFilesCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public LoadAllFilesCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
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
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        foreach (var file in requrement.Files.Where(technicalRequrementsFileWrapper => technicalRequrementsFileWrapper.IsActual))
                        {
                            var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
                            string addToFileName = $"{file.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                            FilesStorage.CopyFileFromStorage(file.Id, MessageService, storageDirectory, dirPath, addToFileName, false);
                        }
                    }

                    Process.Start("explorer.exe", taskPath);
                }
            }

        }
    }
}