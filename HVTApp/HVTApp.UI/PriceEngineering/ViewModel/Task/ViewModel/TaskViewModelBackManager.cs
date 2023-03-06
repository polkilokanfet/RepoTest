using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using HVTApp.UI.PriceEngineering.Tce.Second;
using HVTApp.UI.PriceEngineering.ViewModel;
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

    }
}