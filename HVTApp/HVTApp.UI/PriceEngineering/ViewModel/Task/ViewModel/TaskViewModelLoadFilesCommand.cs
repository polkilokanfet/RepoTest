using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.Services.Storage;
using HVTApp.Model;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelLoadFilesCommand : TaskViewModel
    {
        public ICommandRaiseCanExecuteChanged LoadFilesCommand { get; }

        protected TaskViewModelLoadFilesCommand(IUnityContainer container, Guid priceEngineeringTaskId)
            : base(container, priceEngineeringTaskId)
        {
            LoadFilesCommand = new DelegateLogCommand(
                LoadZipInfo,
                () => true);
        }

        private void LoadZipInfo()
        {
            try
            {
                var filesStorageService = Container.Resolve<IFilesStorageService>();

                //загрузка архива истории проработки
                var zipFilePath = filesStorageService.GetZipFolder(this.Model.GetFileCopyInfoEntities(), $"{Model.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}");
                if (string.IsNullOrEmpty(zipFilePath) != false) return;

                var historyDocumentPath = Container.Resolve<IPrintPriceEngineering>().PrintHistoryPriceEngineeringTask(Model.Id);
                if (string.IsNullOrEmpty(historyDocumentPath) != false) return;

                filesStorageService.AddFilesToZip(zipFilePath, new[] { historyDocumentPath });

                //загрузка отдельных ОЛ
                var actualTechReqFiles = this.Model
                    .GetAllPriceEngineeringTasks()
                    .SelectMany(task => task.FilesTechnicalRequirements)
                    .Where(requirements => requirements.IsActual)
                    .Distinct()
                    .Select(requirements => new FileCopyInfo(requirements, zipFilePath, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath));

                foreach (var fileCopyStorage in actualTechReqFiles)
                {
                    filesStorageService.CopyFileFromStorage(fileCopyStorage.File.Id, fileCopyStorage.SourcePath, Path.GetDirectoryName(zipFilePath), null, false);
                }

                System.Diagnostics.Process.Start(Path.GetDirectoryName(zipFilePath));
            }
            catch (IOException e)
            {
                Container.Resolve<IMessageService>().Message(e.GetType().ToString(), e.Message);
            }
        }
    }
}