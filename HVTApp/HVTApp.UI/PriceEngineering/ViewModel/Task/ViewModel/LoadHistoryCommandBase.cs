using System;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
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

            _fileNameNum = 1;
            var actualTechReqFiles = task1
                .GetAllPriceEngineeringTasks()
                .SelectMany(task => task.FilesTechnicalRequirements)
                .Where(requirements => requirements.IsActual)
                .Distinct()
                .Select(requirements => new FileCopyInfoTechnicalSpecification(requirements, targetDirectoryPath, this.FileName(task1, requirements)));
            foreach (var fileCopyStorage in actualTechReqFiles)
            {
                FilesStorageService.CopyFileFromStorage(
                    fileCopyStorage.TargetName,
                    fileCopyStorage.File.Id, 
                    fileCopyStorage.SourcePath, 
                    Path.Combine(targetDirectoryPath, "Актуальное ТЗ"));
            }
        }

        private int _fileNameNum = 1;
        private string FileName(PriceEngineeringTask task, PriceEngineeringTaskFileTechnicalRequirements requirements)
        {
            var tasks = task.GetAllPriceEngineeringTasks();
            var targetTask = tasks.First(x => x.FilesTechnicalRequirements.Contains(requirements));
            var productCategory = GlobalAppProperties.ProductDesignationService.GetProductCategory(new Product(){ProductBlock = targetTask.ProductBlock});
            return $"{targetTask.Number} {productCategory.NameShort} ({_fileNameNum++})";
        }
    }
}