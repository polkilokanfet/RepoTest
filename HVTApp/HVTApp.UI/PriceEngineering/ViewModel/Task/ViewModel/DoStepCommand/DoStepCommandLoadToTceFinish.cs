using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandLoadToTceFinish : DoStepCommand
    {
        private readonly Action _doAfter;
        protected override ScriptStep Step => ScriptStep.LoadToTceFinish;

        protected override string ConfirmationMessage => "Вы уверены, что загрузили результаты проработки в ТС?";

        public DoStepCommandLoadToTceFinish(TaskViewModelBackManager viewModel, IUnityContainer container, Action doAfter) : base(viewModel, container)
        {
            _doAfter = doAfter;
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());
            yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"ТСП загружено в TeamCenter: {ViewModel.Model}");
        }

        protected override void DoStepAction()
        {
            var tasks = ViewModel.Model.ChildPriceEngineeringTasks
                .SelectMany(task => task.GetAllPriceEngineeringTasks())
                .Where(task => task.Status.Equals(ScriptStep.LoadToTceStart));
            foreach (var task in tasks)
            {
                task.Statuses.Add(new PriceEngineeringTaskStatus()
                {
                    Moment = DateTime.Now,
                    StatusEnum = ScriptStep.LoadToTceFinish.Value
                });
            }

            var vm = (TaskViewModelBackManager)ViewModel;
            vm.TasksTceItem.AcceptChanges();
            base.DoStepAction();
            _doAfter?.Invoke();
        }

        protected override bool CanExecuteMethod()
        {
            var vm = (TaskViewModelBackManager) ViewModel;
            return vm.TasksWrapperBackManager.IsValid && 
                   vm.TasksTceItem.IsValid &&
                   ViewModel.Status.Equals(ScriptStep.LoadToTceStart);
        }
    }
}