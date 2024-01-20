using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public abstract class DoStepCommand : DelegateLogCommand
    {
        public IUnityContainer Container { get; }
        protected readonly TaskViewModel ViewModel;
        private readonly Action _doAfterAction;
        protected readonly IMessageService MessageService;
        protected readonly IEventAggregator EventAggregator;

        protected abstract ScriptStep Step { get; }
        protected abstract string ConfirmationMessage { get; }

        #region ctor

        protected DoStepCommand(TaskViewModel viewModel, IUnityContainer container, Action doAfterAction = null)
        {
            Container = container;
            ViewModel = viewModel;
            MessageService = container.Resolve<IMessageService>();
            EventAggregator = container.Resolve<IEventAggregator>();
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
            var dr = MessageService.ConfirmationDialog("ѕодтверждение", ConfirmationMessage, defaultNo: true);
            if (dr == false) return;
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
        protected abstract IEnumerable<NotificationArgsItem> GetEventServiceItems();

        /// <summary>
        /// ƒобавить ли этот же статус во все вложенные подзадачи
        /// </summary>
        protected virtual bool SetSameStatusOnSubTasks => false;

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
            if (SetSameStatusOnSubTasks)
            {
                foreach (var task in ViewModel.Model.GetAllPriceEngineeringTasks().Where(t => t.Id != ViewModel.Model.Id))
                {
                    task.Statuses.Add(new PriceEngineeringTaskStatus
                    {
                        Moment = status.Moment,
                        StatusEnum = status.StatusEnum,
                        Comment = GetStatusComment()
                    });
                }
            }

            ViewModel.SaveCommand.Execute();
            this.RaiseCanExecuteChanged();
        }

        protected override bool CanExecuteMethod() => ViewModel.IsValid &&
                                                      Step.AllowDoStep(ViewModel.Status);

        protected virtual string GetStatusComment() => null;
    }
}