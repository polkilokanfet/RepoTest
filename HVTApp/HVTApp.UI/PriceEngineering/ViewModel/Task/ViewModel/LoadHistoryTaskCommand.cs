using System;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Services.Storage;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.ViewModel;

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
        /// Загрузка истории проработки в целевую директорию
        /// </summary>
        /// <param name="task1">задача</param>
        /// <param name="targetDirectoryPath">целевая директория</param>
        protected void LoadHistory(PriceEngineeringTask task1, string targetDirectoryPath)
        {
            //загрузка архива истории проработки
            var zipFileName = $"history_{task1.Number:D4}_{DateTime.Now.ToShortDateString()}_{DateTime.Now.ToShortTimeString()}".ReplaceUncorrectSimbols();
            var zipFilePath = FilesStorageService.GetZip(task1.GetFileCopyInfoEntitiesAll(), zipFileName, targetDirectoryPath);

            //история проработки .doc
            var historyDocumentPath = PrintPriceEngineering.PrintHistoryPriceEngineeringTask(task1.Id);
            if (string.IsNullOrEmpty(historyDocumentPath)) return;
            FilesStorageService.AddFilesToZip(zipFilePath, new[] { historyDocumentPath });

            //загрузка отдельных актуальных ОЛ
            if (task1.ParentPriceEngineeringTaskId != null) return;
            var actualTechReqFiles = task1
                .GetAllPriceEngineeringTasks()
                .SelectMany(task => task.FilesTechnicalRequirements)
                .Where(requirements => requirements.IsActual)
                .Distinct()
                .Select(requirements => new FileCopyInfoTechnicalSpecification(requirements, targetDirectoryPath));
            foreach (var fileCopyStorage in actualTechReqFiles)
            {
                FilesStorageService.CopyFileFromStorage(
                    fileCopyStorage.File.Id, 
                    fileCopyStorage.SourcePath, 
                    Path.Combine(targetDirectoryPath, "Актуальное ТЗ"), null, false, true);
            }
        }
    }

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
            //try
            //{
                //директория для сохранения
                var directoryPath = FilesStorageService.GetDirectoryPath();
                if (directoryPath == null) return;

                //загрузка архива истории проработки
                this.LoadHistory(_taskViewModel.Model, directoryPath);

                System.Diagnostics.Process.Start(directoryPath);
            //}
            //catch (IOException e)
            //{
            //    MessageService.Message(e.GetType().ToString(), e.Message);
            //}
        }
    }

    /// <summary>
    /// Загрузка истории проработки всего оборудования из сборки задач
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
                //директория для сохранения
                var directoryPath = FilesStorageService.GetDirectoryPath();
                if (directoryPath == null) return;

                foreach (var task in _getTasks.Invoke().ChildPriceEngineeringTasks)
                {
                    var dp = Path.Combine(directoryPath, task.Number.ToString());
                    //загрузка архива истории проработки
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