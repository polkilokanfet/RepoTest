using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    /// <summary>
    /// ��������� ������������ ��� BackManagerBoss (�������������)
    /// </summary>
    public class DoStepCommandStopProductionRequestConfirm : DoStepCommand<TaskViewModelBackManagerBoss>
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ����������� ��������� ������������ ����� ������������?";

        public DoStepCommandStopProductionRequestConfirm(TaskViewModelBackManagerBoss viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string GetStatusComment()
        {
            return $"��������� ������������ ����������� ({GlobalAppProperties.User}).";
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskProductionRequestStopConfirm,
                RecipientRole = Role.SalesManager,
                RecipientUser = Manager,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override void BeforeDoStepAction()
        {
            foreach (var salesUnit in ViewModel.Model.SalesUnits)
            {
                salesUnit.SignalToStartProduction = null;
                salesUnit.SignalToStartProductionDone = null;
                if (salesUnit.Order != null)
                    salesUnit.Order = null;
            }

            //��������� ����� ����, ��� ������������ �� ��������� � TeamCenter
            foreach (var priceEngineeringTask in ViewModel.Model.GetAllPriceEngineeringTasks())
            {
                priceEngineeringTask.IsUploadedDocumentationToTeamCenter = false;
            }

        }

        protected override bool CanExecuteMethod()
        {
            return
                GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss &&
                ViewModel.Status.Equals(ScriptStep.ProductionRequestStop);
        }
    }
}