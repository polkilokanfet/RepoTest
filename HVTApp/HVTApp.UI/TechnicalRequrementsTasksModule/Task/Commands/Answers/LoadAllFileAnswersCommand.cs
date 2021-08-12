using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class LoadAllFileAnswersCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public LoadAllFileAnswersCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            using (var fdb = new FolderBrowserDialog())
            {
                var result = fdb.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                {
                    var targetDirectoryPath = fdb.SelectedPath;

                    foreach (var answerFile in ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles)
                    {
                        var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;
                        string addToFileName = $"{answerFile.Name.ReplaceUncorrectSimbols().LimitLengh()}";
                        FilesStorage.CopyFileFromStorage(answerFile.Id, MessageService, storageDirectory, targetDirectoryPath, addToFileName, false);
                    }

                    Process.Start("explorer.exe", targetDirectoryPath);
                }
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles.Any();
        }
    }
}