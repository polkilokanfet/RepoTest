using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class OpenFileCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public OpenFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            try
            {
                TechnicalRequrementsFileWrapper fileWrapper = (TechnicalRequrementsFileWrapper)ViewModel.SelectedItem;
                FilesStorage.OpenFileFromStorage(fileWrapper.Id, MessageService, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath, fileWrapper.Name);
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedItem is TechnicalRequrementsFileWrapper;
        }
    }
}