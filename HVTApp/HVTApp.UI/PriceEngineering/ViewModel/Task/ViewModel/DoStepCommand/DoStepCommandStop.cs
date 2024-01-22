using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// Остановить проработку задачи
    /// </summary>
    public class DoStepCommandStop : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "Вы уверены, что хотите остановить проработку задачи?";

        public DoStepCommandStop(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationItem> GetEventServiceItems()
        {
            if (ViewModel.Model.UserConstructor != null)
            {
                yield return new NotificationItem(ViewModel.Model.UserConstructor, Role.Constructor, $"ТСП остановлена: {ViewModel.Model}");
            }
        }

        protected override void BeforeDoStepAction()
        {
            foreach (var salesUnit in ViewModel.SalesUnits)
            {
                salesUnit.SignalToStartProduction = null;
                salesUnit.SignalToStartProductionDone = null;
            }
        }
    }
}