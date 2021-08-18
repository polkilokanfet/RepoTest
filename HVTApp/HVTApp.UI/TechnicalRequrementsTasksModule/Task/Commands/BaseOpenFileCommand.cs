using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public abstract class BaseOpenFileCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public abstract Guid GetFileId { get; }
        public abstract string GetFileName { get; }
        public abstract string GetFilePath { get; }

        protected BaseOpenFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            try
            {
                FilesStorage.OpenFileFromStorage(GetFileId, MessageService, GetFilePath, GetFileName);
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
            }
        }
    }
}