using System;
using HVTApp.Infrastructure;
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
                FilesStorage.OpenFileFromStorage(ViewModel.SelectedAnswerFile.Id, MessageService, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath, ViewModel.SelectedAnswerFile.Name);
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedAnswerFile != null;
        }
    }
}