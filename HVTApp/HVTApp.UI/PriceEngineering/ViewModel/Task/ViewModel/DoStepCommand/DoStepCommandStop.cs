using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// ���������� ���������� ������
    /// </summary>
    public class DoStepCommandStop : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ���������� ���������� ������?";

        public DoStepCommandStop(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if (ViewModel.Model.UserConstructor != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = EventServiceActionType.PriceEngineeringTaskStop,
                    RecipientRole = Role.Constructor,
                    RecipientUser = ViewModel.Model.UserConstructor,
                    TargetEntityId = ViewModel.Model.Id
                };
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