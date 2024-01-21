using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
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

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override void BeforeDoStepAction()
        {
            var vm = (TaskViewModelPlanMaker) ViewModel;
            var ordersGroups = vm.SalesUnits.GroupBy(x => new {OrderNumber = x.OrderNumber.Trim(), x.DateOpen});
            foreach (var ordersGroup in ordersGroups)
            {
                var order = new Order {DateOpen = ordersGroup.Key.DateOpen, Number = ordersGroup.Key.OrderNumber};
                EnumerableExtensions.ForEach(ordersGroup, x => x.Model.Order = order);
            }
            vm.SignalToStartProductionDone = DateTime.Now;
        }

        protected override string GetStatusComment()
        {
            var vm = (TaskViewModelPlanMaker)ViewModel;
            return $"з/з {vm.SalesUnits.Select(x => x.OrderNumber.Trim()).Distinct().ToStringEnum()}";
        }
    }
}