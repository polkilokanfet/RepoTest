using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using HVTApp.UI.PriceEngineering.Tce.Second;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManager : TaskViewModel
    {
        public TasksWrapperBackManager TasksWrapperBackManager { get; }

        public override bool IsTarget => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        public override bool IsEditMode => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        public TasksTceItem TasksTceItem { get; }

        #region Commands

        public ICommandRaiseCanExecuteChanged LoadToTceFinishCommand { get; }
        public ICommandRaiseCanExecuteChanged LoadFilesCommand { get; }

        #endregion

        public OrderWrapper Order { get; }

        public event Action SavedEvent;
        public event Action LoadToTceFinishedEvent;

        public TaskViewModelBackManager(TasksWrapperBackManager tasksWrapperBackManager, IUnityContainer container, Guid priceEngineeringTaskId) 
            : base(container, priceEngineeringTaskId)
        {
            TasksWrapperBackManager = tasksWrapperBackManager;
            TasksTceItem = new TasksTceItem(this.Model);

            TasksTceItem.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
                LoadToTceFinishCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(IsEditMode));
                LoadToTceFinishCommand.RaiseCanExecuteChanged();
            };

            this.TasksWrapperBackManager.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
                LoadToTceFinishCommand.RaiseCanExecuteChanged();
            };

            LoadToTceFinishCommand = new DoStepCommandLoadToTceFinish(this, container, () => LoadToTceFinishedEvent?.Invoke());
            LoadFilesCommand = new DelegateLogCommand(
                LoadZipInfo,
                () => Model.Status.Equals(ScriptStep.ProductionRequestStart) || Model.Status.Equals(ScriptStep.ProductionRequestFinish));

            Order = this.Model.SalesUnits.FirstOrDefault()?.Order == null
                ? null
                : new OrderWrapper(this.Model.SalesUnits.FirstOrDefault().Order);
        }

        protected override void SaveCommand_ExecuteMethod()
        {
            TasksTceItem.AcceptChanges();
            UnitOfWork.SaveChanges();
            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
            SaveCommand.RaiseCanExecuteChanged();
            SavedEvent?.Invoke();
        }

        protected override bool SaveCommand_CanExecuteMethod()
        {
            if (this.Model.Status.Equals(ScriptStep.LoadToTceStart) == false)
                return false;
            return TasksTceItem.IsChanged || this.TasksWrapperBackManager.IsChanged;
        }

        private void LoadZipInfo()
        {
            try
            {
                var filesStorageService = Container.Resolve<IFilesStorageService>();

                //загрузка архива истории проработки
                var zipFilePath = filesStorageService.GetZipFolder(GetFileCopyStorages(), $"{Model.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}");
                if (string.IsNullOrEmpty(zipFilePath) == false)
                {
                    var historyDocumentPath = Container.Resolve<IPrintPriceEngineering>().PrintPriceEngineeringTask(Model.Id);
                    if (string.IsNullOrEmpty(historyDocumentPath) == false)
                    {
                        filesStorageService.AddFilesToZip(zipFilePath, new[] { historyDocumentPath });

                        //загрузка отдельных ОЛ
                        var actualTechReqFiles = this.Model
                            .GetAllPriceEngineeringTasks()
                            .SelectMany(x => x.FilesTechnicalRequirements)
                            .Where(x => x.IsActual)
                            .Distinct()
                            .Select(x => new FileCopyStorage(x, zipFilePath, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath));

                        foreach (var fileCopyStorage in actualTechReqFiles)
                        {
                            filesStorageService.CopyFileFromStorage(fileCopyStorage.File.Id, fileCopyStorage.SourcePath, Path.GetDirectoryName(zipFilePath), null, false);
                        }

                        System.Diagnostics.Process.Start(Path.GetDirectoryName(zipFilePath));
                    }
                }
            }
            catch (IOException e)
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.Message);
            }
        }

        private IEnumerable<IFileCopyStorage> GetFileCopyStorages()
        {
            foreach (var pet in this.Model.GetAllPriceEngineeringTasks().ToList())
            {
                foreach (var fileTechnicalRequirement in pet.FilesTechnicalRequirements)
                {
                    yield return new FileCopyStorage(fileTechnicalRequirement, $"{pet.GetDirectoryName()}-TechReq", GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
                }

                foreach (var answer in pet.FilesAnswers)
                {
                    yield return new FileCopyStorage(answer, $"{pet.GetDirectoryName()}-Answer", GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
                }
            }
        }

        private class FileCopyStorage : IFileCopyStorage
        {
            public IFileStorage File { get; }
            public string DestinationDirectoryName { get; }
            public string SourcePath { get; }

            public FileCopyStorage(IFileStorage file, string destinationDirectoryName, string sourcePath)
            {
                File = file;
                DestinationDirectoryName = destinationDirectoryName;
                SourcePath = sourcePath;
            }

            public override bool Equals(object obj)
            {
                if (obj is IFileCopyStorage other)
                {
                    return this.File.Id == other.File.Id;
                }
                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (File != null ? File.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (DestinationDirectoryName != null ? DestinationDirectoryName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (SourcePath != null ? SourcePath.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

    }
}