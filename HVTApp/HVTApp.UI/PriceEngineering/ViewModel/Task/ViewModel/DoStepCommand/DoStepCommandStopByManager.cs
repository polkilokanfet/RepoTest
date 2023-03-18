using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStopByManager : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "Вы уверены, что хотите остановить проработку задачи?";

        public DoStepCommandStopByManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());
            if (ViewModel.Model.UserConstructor != null)
            {
                yield return new NotificationArgsItem(ViewModel.Model.UserConstructor, Role.Constructor, $"ТСП остановлена: {ViewModel.Model}");
            }
        }

        protected override void DoStepAction()
        {
            foreach (var salesUnit in ViewModel.SalesUnits)
            {
                salesUnit.SignalToStartProduction = null;
                salesUnit.SignalToStartProductionDone = null;
            }
            base.DoStepAction();
        }
    }
}