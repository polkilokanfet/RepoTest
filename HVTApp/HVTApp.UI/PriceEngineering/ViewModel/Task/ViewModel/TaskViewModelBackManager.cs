using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
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

        #region Order

        public OrderWrapper Order => SalesUnits.First().Order;

        public new SalesUnitsCollection SalesUnits { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            base.InitializeCollectionProperties();

            if (Model.SalesUnits == null) throw new ArgumentException("SU cannot be null");
            SalesUnits = new SalesUnitsCollection(this.Model, Model.Status.Equals(ScriptStep.ProductionRequestStart));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }

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

            if (Model.Status.Equals(ScriptStep.ProductionRequestStart) && Order == null)
            {
                SalesUnits.AddOrder(new Order(){DateOpen = DateTime.Now});
            }
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

        public class SalesUnitWithOrderWrapper : WrapperBase<SalesUnit>
        {
            private readonly bool _orderIsRequired;

            #region SimpleProperties

            /// <summary>
            /// Сигнал менеджера о производстве
            /// </summary>
            public DateTime SignalToStartProduction
            {
                get { return GetValue<DateTime>(); }
                set { SetValue(value); }
            }
            public DateTime SignalToStartProductionOriginalValue => GetOriginalValue<DateTime>(nameof(SignalToStartProduction));
            public bool SignalToStartProductionIsChanged => GetIsChanged(nameof(SignalToStartProduction));

            /// <summary>
            /// Дата размещения в производстве
            /// </summary>
            public DateTime SignalToStartProductionDone
            {
                get { return GetValue<DateTime>(); }
                set { SetValue(value); }
            }
            public DateTime SignalToStartProductionDoneOriginalValue => GetOriginalValue<DateTime>(nameof(SignalToStartProductionDone));
            public bool SignalToStartProductionDoneIsChanged => GetIsChanged(nameof(SignalToStartProductionDone));

            #endregion

            #region ComplexProperties

            /// <summary>
            /// Заказ
            /// </summary>
	        public OrderWrapper Order
            {
                get { return GetWrapper<OrderWrapper>(); }
                set { SetComplexValue<Order, OrderWrapper>(Order, value); }
            }

            #endregion

            public SalesUnitWithOrderWrapper(SalesUnit model, bool orderIsRequired) : base(model)
            {
                _orderIsRequired = orderIsRequired;
            }

            public override void InitializeComplexProperties()
            {
                InitializeComplexProperty(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));
            }

            protected override IEnumerable<ValidationResult> ValidateOther()
            {
                if (_orderIsRequired)
                {
                    if(Order == null)
                        yield return new ValidationResult($"{nameof(Order)} is required", new[] {nameof(Order)});
                }
            }
        }

        public class SalesUnitsCollection : ValidatableChangeTrackingCollection<SalesUnitWithOrderWrapper>
        {
            public SalesUnitsCollection(PriceEngineeringTask priceEngineeringTask, bool orderIsRequired) 
                : base(priceEngineeringTask.SalesUnits.Select(salesUnit => new SalesUnitWithOrderWrapper(salesUnit, orderIsRequired)))
            {
            }

            public void AddOrder(Order order)
            {
                var orderWrapper = new OrderWrapper(order);
                this.Items.ForEach(x => x.Order = orderWrapper);
            }
        }
    }
}