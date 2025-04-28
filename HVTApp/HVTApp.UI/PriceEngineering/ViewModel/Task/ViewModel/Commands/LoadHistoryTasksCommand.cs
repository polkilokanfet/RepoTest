using System;
using System.IO;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.UI.PriceEngineering
{
    /// <summary>
    /// �������� ������� ���������� ����� ������������ �� ������ �����
    /// </summary>
    public class LoadHistoryTasksCommand : LoadHistoryCommandBase
    {
        private readonly Func<PriceEngineeringTasks> _getTasks;

        public LoadHistoryTasksCommand(
            Func<PriceEngineeringTasks> getTasks,
            IFilesStorageService filesStorageService,
            IPrintPriceEngineering printPriceEngineering,
            IMessageService messageService) : base(filesStorageService, printPriceEngineering, messageService)
        {
            _getTasks = getTasks;
        }

        protected override void ExecuteMethod()
        {
            try
            {
                //���������� ��� ����������
                var directoryPath = FilesStorageService.GetDirectoryPath();
                if (string.IsNullOrEmpty(directoryPath)) return;

                foreach (var task in _getTasks.Invoke().ChildPriceEngineeringTasks)
                {
                    var dp = Path.Combine(directoryPath, task.Number.ToString());
                    Directory.CreateDirectory(dp);
                    //�������� ������ ������� ����������
                    this.LoadHistory(task, dp);
                }

                System.Diagnostics.Process.Start(directoryPath);
            }
            catch (IOException e)
            {
                MessageService.Message(e.GetType().ToString(), e.Message);
            }
        }
    }
}