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
        /// <param name="targetDirectoryPath">������� ����������</param>
        protected void LoadHistory(PriceEngineeringTask task1, string targetDirectoryPath)
        {
            //�������� ������ ������� ����������
            var zipFileName = $"history_{task1.Number:D4}_{DateTime.Now.ToShortDateString()}_{DateTime.Now.ToShortTimeString()}".ReplaceUncorrectSimbols();
            var zipFilePath = FilesStorageService.GetZip(task1.GetFileCopyInfoEntitiesAll(), zipFileName, targetDirectoryPath);

            //������� ���������� .doc
            var historyDocumentPath = PrintPriceEngineering.PrintHistoryPriceEngineeringTask(task1.Id);
            if (string.IsNullOrEmpty(historyDocumentPath)) return;
            FilesStorageService.AddFilesToZip(zipFilePath, new[] { historyDocumentPath });

            //�������� ��������� ���������� ��
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
                    Path.Combine(targetDirectoryPath, "���������� ��"), null, false, true);
            }
        }
    }
}