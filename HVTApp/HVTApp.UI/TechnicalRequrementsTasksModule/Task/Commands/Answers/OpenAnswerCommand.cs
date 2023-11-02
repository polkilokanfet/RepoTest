using System;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class OpenAnswerCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public OpenAnswerCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            try
            {
                FilesStorageService.OpenFileFromStorage(ViewModel.SelectedAnswerFile.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath, ViewModel.SelectedAnswerFile.Name);
            }
            catch (Exception e)
            {
                MessageService.Message("Exception", e.PrintAllExceptions());
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedAnswerFile != null;
        }
    }
}