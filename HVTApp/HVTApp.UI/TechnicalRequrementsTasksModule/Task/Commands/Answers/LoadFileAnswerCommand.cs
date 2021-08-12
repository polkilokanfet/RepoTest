using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class LoadFileAnswerCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public LoadFileAnswerCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;
            string addToFileName = $"{ViewModel.SelectedAnswerFile.Name.ReplaceUncorrectSimbols().LimitLengh()}";
            FilesStorage.CopyFileFromStorage(ViewModel.SelectedAnswerFile.Id, MessageService, storageDirectory, addToFileName: addToFileName);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedAnswerFile != null;
        }
    }
}