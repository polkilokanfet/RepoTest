using System;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Services.Storage;
using HVTApp.UI.Commands;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class LoadHistoryCommandBase : DelegateLogCommand
    {
        protected readonly IFilesStorageService FilesStorageService;
        protected readonly IPrintPriceEngineering PrintPriceEngineering;
        protected readonly IMessageService MessageService;

        protected LoadHistoryCommandBase(
            IFilesStorageService filesStorageService,
            IPrintPriceEngineering printPriceEngineering,
            IMessageService messageService)
        {
            FilesStorageService = filesStorageService;
            PrintPriceEngineering = printPriceEngineering;
            MessageService = messageService;
        }

        /// <summary>
        /// �������� ������� ���������� � ������� ����������
        /// </summary>
        /// <param name="task1">������</param>
        /// <param name="directoryPath">������� ����������</param>
        protected void LoadHistory(PriceEngineeringTask task1, string directoryPath)
        {
            //�������� ������ ������� ����������
            var zipFileName = $"{task1.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}";
            var zipFilePath = FilesStorageService.GetZip(task1.GetFileCopyInfoEntities(), zipFileName, directoryPath);

            //������� ���������� .doc
            var historyDocumentPath = PrintPriceEngineering.PrintHistoryPriceEngineeringTask(task1.Id);
            if (string.IsNullOrEmpty(historyDocumentPath)) return;
            FilesStorageService.AddFilesToZip(zipFilePath, new[] { historyDocumentPath });

            //�������� ��������� ��
            if (task1.ParentPriceEngineeringTaskId != null) return;
            var actualTechReqFiles = task1
                .GetAllPriceEngineeringTasks()
                .SelectMany(task => task.FilesTechnicalRequirements)
                .Where(requirements => requirements.IsActual)
                .Distinct()
                .Select(requirements => new FileCopyInfoTechnicalSpecification(requirements, zipFilePath));
            foreach (var fileCopyStorage in actualTechReqFiles)
            {
                FilesStorageService.CopyFileFromStorage(fileCopyStorage.File.Id, fileCopyStorage.SourcePath, Path.GetDirectoryName(zipFilePath), null, false);
            }
        }
    }

    public class LoadHistoryTaskCommand : LoadHistoryCommandBase
    {
        private readonly PriceEngineeringTask _task;

        public LoadHistoryTaskCommand(
            PriceEngineeringTask task, 
            IFilesStorageService filesStorageService, 
            IPrintPriceEngineering printPriceEngineering,
            IMessageService messageService) : base(filesStorageService, printPriceEngineering, messageService)
        {
            _task = task;
        }

        protected override void ExecuteMethod()
        {
            try
            {
                //���������� ��� ����������
                var directoryPath = FilesStorageService.GetDirectoryPath();
                if (directoryPath == null) return;

                //�������� ������ ������� ����������
                this.LoadHistory(_task, directoryPath);

                System.Diagnostics.Process.Start(directoryPath);
            }
            catch (IOException e)
            {
                MessageService.Message(e.GetType().ToString(), e.Message);
            }
        }
    }

    public class LoadHistoryTasksCommand : LoadHistoryCommandBase
    {
        private readonly PriceEngineeringTasks _tasks;

        public LoadHistoryTasksCommand(
            PriceEngineeringTasks tasks,
            IFilesStorageService filesStorageService,
            IPrintPriceEngineering printPriceEngineering,
            IMessageService messageService) : base(filesStorageService, printPriceEngineering, messageService)
        {
            _tasks = tasks;
        }

        protected override void ExecuteMethod()
        {
            try
            {
                //���������� ��� ����������
                var directoryPath = FilesStorageService.GetDirectoryPath();
                if (directoryPath == null) return;

                foreach (var task in _tasks.ChildPriceEngineeringTasks)
                {
                    var dp = Path.Combine(directoryPath, task.Number.ToString());
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