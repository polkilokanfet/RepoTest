using System.IO;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Services;

namespace HVTApp.UI.PriceEngineering
{
    public class LoadHistoryTaskCommand : LoadHistoryCommandBase
    {
        private readonly TaskViewModel _taskViewModel;

        public LoadHistoryTaskCommand(
            TaskViewModel taskViewModel, 
            IFilesStorageService filesStorageService, 
            IPrintPriceEngineering printPriceEngineering,
            IMessageService messageService) : base(filesStorageService, printPriceEngineering, messageService)
        {
            _taskViewModel = taskViewModel;
        }

        protected override void ExecuteMethod()
        {
            try
            {
                //���������� ��� ����������
                var directoryPath = FilesStorageService.GetDirectoryPath();
                if (string.IsNullOrEmpty(directoryPath)) return;

                //�������� ������ ������� ����������
                this.LoadHistory(_taskViewModel.Model, directoryPath);

                System.Diagnostics.Process.Start(directoryPath);
            }
            catch (IOException e)
            {
                MessageService.Message(e.GetType().ToString(), e.Message);
            }
        }
    }
}