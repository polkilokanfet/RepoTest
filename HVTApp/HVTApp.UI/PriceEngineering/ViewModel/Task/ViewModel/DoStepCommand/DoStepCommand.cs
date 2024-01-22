using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public abstract class DoStepCommand<TTaskViewModel> : DelegateLogCommand, IDisposable
    where TTaskViewModel : TaskViewModel
    {
        public IUnityContainer Container { get; }
        protected readonly TTaskViewModel ViewModel;
        private readonly Action _doAfterAction;
        protected readonly IMessageService MessageService;
        protected readonly IEventAggregator EventAggregator;

        protected abstract ScriptStep Step { get; }
        protected abstract string ConfirmationMessage { get; }

        protected readonly IUnitOfWork UnitOfWork;

        protected User Manager => ViewModel.Model.GetPriceEngineeringTasks(UnitOfWork).UserManager;

        #region ctor

        protected DoStepCommand(TTaskViewModel viewModel, IUnityContainer container, Action doAfterAction = null)
        {
            Container = container;
            ViewModel = viewModel;
            MessageService = container.Resolve<IMessageService>();
            EventAggregator = container.Resolve<IEventAggregator>();
            UnitOfWork = container.Resolve<IUnitOfWork>();
            _doAfterAction = doAfterAction;
        }

        #endregion

        /// <summary>
        /// ¬ыполнить команду без подтверждени€ от пользовател€
        /// </summary>
        public void ExecuteWithoutConfirmation()
        {
            if (this.AllowDoStepAction() == false) return;
            this.BeforeDoStepAction();
            this.DoStepAction();
            this.SendNotification();
            _doAfterAction?.Invoke();
        }

        protected override void ExecuteMethod()
        {
            if (MessageService.ConfirmationDialog("ѕодтверждение", ConfirmationMessage, defaultNo: true) == false) return;
            this.ExecuteWithoutConfirmation();
        }

        /// <summary>
        /// ќтправка уведомлений
        /// </summary>
        protected virtual void SendNotification()
        {
            this.EventAggregator.GetEvent<PriceEngineeringTaskNotificationEvent>()
                .Publish(new NotificationArgsPriceEngineeringTask(this.ViewModel.Model, this.GetEventServiceItems()));
        }

        /// <summary>
        /// ‘ормирование аргументов дл€ отправки уведомлений
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<NotificationItem> GetEventServiceItems();

        /// <summary>
        /// ƒобавить ли этот же статус во все вложенные подзадачи
        /// </summary>
        protected virtual bool NeedAddSameStatusOnSubTasks => false;

        /// <summary>
        /// ѕроверка на выполнение всех необходимых условий дл€ применени€ основного действи€
        /// </summary>
        /// <returns></returns>
        protected virtual bool AllowDoStepAction() => true;

        /// <summary>
        /// ѕредварительное действие по переходу на новый статус
        /// </summary>
        protected virtual void BeforeDoStepAction() { }

        /// <summary>
        /// ќсновное действие по переходу на новый статус
        /// </summary>
        protected virtual void DoStepAction()
        {
            var status = ViewModel.Statuses.Add(this.Step, this.GetStatusComment());
            //установка подобного статуса во все вложенные задачи
            if (NeedAddSameStatusOnSubTasks) this.AddSameStatusOnSubTasks(status);
            ViewModel.SaveCommand.Execute();
            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// ”становка подобного статуса во все вложенные задачи
        /// </summary>
        /// <param name="status"></param>
        private void AddSameStatusOnSubTasks(PriceEngineeringTaskStatus status)
        {
            foreach (var task in ViewModel.Model.GetAllPriceEngineeringTasks().Where(priceEngineeringTask => priceEngineeringTask.Id != ViewModel.Model.Id))
            {
                task.Statuses.Add(new PriceEngineeringTaskStatus
                {
                    Moment = status.Moment,
                    StatusEnum = status.StatusEnum,
                    Comment = status.Comment
                });
            }
        }

        protected override bool CanExecuteMethod() => ViewModel.IsValid &&
                                                      Step.AllowDoStep(ViewModel.Status);

        protected virtual string GetStatusComment() => null;

        public void Dispose()
        {
            UnitOfWork?.Dispose();
        }
    }
}