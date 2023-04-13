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
    public class DoStepCommandProductionRequestFinish : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "Вы уверены, что открыли производство?";

        public DoStepCommandProductionRequestFinish(TaskViewModel viewModel, IUnityContainer container, Action doAfter) : base(viewModel, container, doAfter)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());
            yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"Производство открыто: {ViewModel.Model}");
        }

        protected override bool SetSameStatusOnSubTasks => true;

        protected override void DoStepAction()
        {
            var vm = (TaskViewModelPlanMaker) ViewModel;
            vm.SignalToStartProductionDone = DateTime.Now;
            base.DoStepAction();
        }

        protected override string GetStatusComment()
        {
            var vm = (TaskViewModelPlanMaker)ViewModel;
            return $"з/з {vm.Order.Number}";
        }
    }
}