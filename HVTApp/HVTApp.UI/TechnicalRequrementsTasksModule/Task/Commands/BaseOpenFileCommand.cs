using System;
using HVTApp.Infrastructure.Extansions;
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
                FilesStorageService.OpenFileFromStorage(GetFileId, GetFilePath, GetFileName);
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Ошибка при открытии файла.", e.PrintAllExceptions());
            }
        }
    }
}