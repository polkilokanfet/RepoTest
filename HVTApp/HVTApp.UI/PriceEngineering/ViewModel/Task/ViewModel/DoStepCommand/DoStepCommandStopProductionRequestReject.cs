using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// ��������� ������������ ��� BackManagerBoss (����������)
    /// </summary>
    public class DoStepCommandStopProductionRequestReject : DoStepCommand<TaskViewModelBackManagerBoss>
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ������ �� ��������� ������������ ����� ������������?";

        public DoStepCommandStopProductionRequestReject(TaskViewModelBackManagerBoss viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            yield return new NotificationAboutPriceEngineeringTaskEventArg.StopProductionRequestReject(ViewModel.Model, Manager);
        }

        protected override string GetStatusComment()
        {
            return $"������ �� ��������� ������������ ������� ({GlobalAppProperties.User}).";
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override bool CanExecuteMethod()
        {
            return
                GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss &&
                ViewModel.Status.Equals(ScriptStep.ProductionRequestStop);
        }
    }
}