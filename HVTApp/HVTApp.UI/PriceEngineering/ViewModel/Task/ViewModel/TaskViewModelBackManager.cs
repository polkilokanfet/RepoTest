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
            return TasksTceItem.IsChanged;
        }

        private void LoadZipInfo()
        {
            IList<IFileCopyStorage> files = new List<IFileCopyStorage>();
            foreach (var pet in this.Model.GetAllPriceEngineeringTasks().ToList())
            {
                foreach (var fileTechnicalRequirement in pet.FilesTechnicalRequirements)
                {
                    files.Add(new FileCopyStorage(fileTechnicalRequirement, $"{pet.GetDirectoryName()}-TechReq", GlobalAppProperties.Actual.TechnicalRequrementsFilesPath));
                }

                foreach (var answer in pet.FilesAnswers)
                {
                    files.Add(new FileCopyStorage(answer, $"{pet.GetDirectoryName()}-Answer", GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath));
                }
            }

            var filesStorageService = Container.Resolve<IFilesStorageService>();
            try
            {
                var zipFilePath = filesStorageService.GetZipFolder(files, $"{Model.Number}_{DateTime.Now.ToShortDateString().ReplaceUncorrectSimbols()}");
                if (string.IsNullOrEmpty(zipFilePath) == false)
                {
                    var rr = Container.Resolve<IPrintPriceEngineering>().PrintPriceEngineeringTask(Model.Id);
                    if (string.IsNullOrEmpty(rr) == false)
                    {
                        filesStorageService.AddFilesToZip(zipFilePath, new[] { rr });
                        System.Diagnostics.Process.Start(Path.GetDirectoryName(zipFilePath));
                    }
                }
            }
            catch (IOException e)
            {
                Container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.Message);
            }
        }

        private class FileCopyStorage : IFileCopyStorage
        {
            public IFileStorage File { get; }
            public string DestinationDirectoryName { get; }
            public string SourcePath { get; }

            public FileCopyStorage(IFileStorage file, string destinationDirectoryNameName, string sourcePath)
            {
                File = file;
                DestinationDirectoryName = destinationDirectoryNameName;
                SourcePath = sourcePath;
            }
        }

    }
}