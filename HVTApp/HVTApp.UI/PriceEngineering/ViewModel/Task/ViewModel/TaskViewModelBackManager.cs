using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using HVTApp.UI.PriceEngineering.Tce.Second;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManager : TaskViewModelLoadFilesCommand
    {
        public TasksWrapperBackManager TasksWrapperBackManager { get; }

        public override bool IsTarget => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        public override bool IsEditMode => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        /// <summary>
        /// Сборка задач ТСП в которую входит эта задача
        /// </summary>
        public TasksTceItem TasksTceItem { get; }

        #region Commands

        public ICommandRaiseCanExecuteChanged LoadToTceFinishCommand { get; }

        #endregion

        public event Action LoadToTceFinishedEvent;

        public TaskViewModelBackManager(TasksWrapperBackManager tasksWrapperBackManager, IUnityContainer container, Guid priceEngineeringTaskId) 
            : base(container, priceEngineeringTaskId)
        {
            TasksWrapperBackManager = tasksWrapperBackManager;
            TasksTceItem = new TasksTceItem(this.Model);

            LoadToTceFinishCommand = new DoStepCommandLoadToTceFinish(this, container, () => LoadToTceFinishedEvent?.Invoke());

            this.TasksTceItem.PropertyChanged += TasksTceItemOnPropertyChanged;
            this.Statuses.CollectionChanged += StatusesOnCollectionChanged;
            this.TasksWrapperBackManager.PropertyChanged += TasksTceItemOnPropertyChanged;
            this.SavedEvent += OnSavedEvent; //Костыль для фиксации изменений после завершения этапа "Задача загружена с ТСЕ"
        }

        #region OnEvents

        private void OnSavedEvent()
        {
            if (string.IsNullOrWhiteSpace(_changesMessage) == false)
                this.Messenger.SendMessage(_changesMessage);

            _changesMessage = null;
        }

        private void StatusesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsEditMode));
            LoadToTceFinishCommand.RaiseCanExecuteChanged();
        }

        private void TasksTceItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
            LoadToTceFinishCommand.RaiseCanExecuteChanged();
        }
        
        #endregion

        public override void AcceptChanges()
        {
            if (this.IsEditMode == false && this.Statuses.IsChanged == false)
                _changesMessage = this.GetChangesMessage(); //Костыль для фиксации изменений после завершения этапа "Задача загружена с ТСЕ"

            TasksTceItem.AcceptChanges();
            base.AcceptChanges();
        }

        protected override bool SaveCommand_CanExecuteMethod()
        {
            //if (this.IsEditMode == false) return false;
            if (this.IsEditMode == false && 
                (TasksTceItem.IsValid == false || this.TasksWrapperBackManager.IsValid == false))
                return false;

            return TasksTceItem.IsChanged || this.TasksWrapperBackManager.IsChanged;
        }

        #region Костыль для фиксации изменений после завершения этапа "Задача загружена с ТСЕ"

        private string _changesMessage = null;

        private string GetChangesMessage()
        {
            if (this.TasksTceItem.IsChanged == false &&
                this.TasksWrapperBackManager.IsChanged == false)
                return null;

            var sb = new StringBuilder();
            sb.AppendLine("Внесены изменения в TeamCenter:");

            if (this.TasksWrapperBackManager.TceNumberIsChanged)
                sb.AppendLine($" - заявка: {this.TasksWrapperBackManager.TceNumberOriginalValue} => {this.TasksWrapperBackManager.TceNumber}");

            if (this.TasksTceItem.TcePositionIsChanged)
                sb.AppendLine($" - позиция: {this.TasksTceItem.TcePositionOriginalValue} => {this.TasksTceItem.TcePosition}");

            var modifiedSccVersions = this.TasksTceItem.SccVersions.Where(x => x.IsChanged).ToList();
            foreach (var sccVersion in modifiedSccVersions)
            {
                sb.AppendLine($" - версия scc: {sccVersion.VersionOriginalValue} => {sccVersion.Version}");
            }

            return sb.ToString();
        }

        #endregion

        public override void Dispose()
        {
            TasksTceItem.PropertyChanged -= TasksTceItemOnPropertyChanged;
            this.Statuses.CollectionChanged -= StatusesOnCollectionChanged;
            this.TasksWrapperBackManager.PropertyChanged -= TasksTceItemOnPropertyChanged;
            this.SavedEvent -= OnSavedEvent;

            base.Dispose();
        }
    }
}