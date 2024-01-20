using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandLoadToTceStart : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.LoadToTceStart;

        protected override string ConfirmationMessage => "Вы хотите загрузить результаты проработки в TeamCenter?";

        public DoStepCommandLoadToTceStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void SendNotification()
        {
            if (this.ViewModel.Model.ParentPriceEngineeringTaskId.HasValue == false)
                this.EventAggregator.GetEvent<PriceEngineeringTaskNotificationEvent>().Publish(new NotificationArgsPriceEngineeringTask(this.ViewModel.Model, this.GetEventServiceItems()));
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(unitOfWork);
            if (tasks.BackManager == null)
            {
                foreach (var user in unitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
                {
                    yield return new NotificationArgsItem(user, Role.BackManagerBoss, $"Поручите загрузку в ТС: {ViewModel.Model}");
                }
            }
            else
            {
                yield return new NotificationArgsItem(tasks.BackManager, Role.BackManager, $"Загрузите в ТС: {ViewModel.Model}");
            }
        }

        protected override bool AllowDoStepAction()
        {
            var steps = new [] {ScriptStep.Accept, ScriptStep.LoadToTceStart, ScriptStep.LoadToTceFinish};
            var tasks = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(ViewModel.Model.Id).GetAllPriceEngineeringTasks().ToList();
            var notAccepted = tasks.Where(task => steps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any() == false) return true;
            MessageService.Message("Отказ", $"Сначала примите блоки:\n{notAccepted.Select(task => task.ProductBlock).ToStringEnum()}");
            return false;
        }

        protected override void BeforeDoStepAction()
        {
             #region CheckActualUsers

            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(unitOfWork);
            if (tasks.BackManager?.IsActual == false)
            {
                tasks.BackManager = null;
                unitOfWork.SaveChanges();
                MessageService.Message("Информация", "Back-manager удален из задачи, т.к. его профиль не актуален");
                this.ViewModel.Messenger.SendMessage("Back-manager удален из задачи, т.к. его профиль не актуален. Необходимо назначить другого.");
            }

            #endregion

           foreach (var childPriceEngineeringTask in this.ViewModel.ChildPriceEngineeringTasks)
            {
                if (childPriceEngineeringTask is TaskViewModelManagerOld task)
                {
                    if (task.LoadToTceStartCommand.CanExecute(null))
                    {
                        ((DoStepCommandLoadToTceStart)task.LoadToTceStartCommand).ExecuteWithoutConfirmation();
                    }
                }
                else
                {
                    throw new ArgumentException("Неверный тип задачи");
                }
            }
        }

        protected override bool CanExecuteMethod()
        {
            return 
                !this.ViewModel.Model.GetAllPriceEngineeringTasks().All(task => ScriptStep.LoadToTceFinish.Equals(ViewModel.Model.Status)) && 
                base.CanExecuteMethod();
        }
    }
}