using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public abstract class DoStepCommandAcceptByDesignDepartmentBase<TViewModel> : DoStepCommand<TViewModel> 
        where TViewModel : TaskViewModelBaseInspector
    {
        protected override ScriptStep Step => ScriptStep.VerificationAccept;
        protected override string ConfirmationMessage => "Вы уверены, что хотите принять результаты проработки?";

        protected abstract User Inspector { get; }

        protected DoStepCommandAcceptByDesignDepartmentBase(TViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskVerificationAcceptedByDesignDepartment,
                RecipientRole = Role.SalesManager,
                RecipientUser = Manager,
                TargetEntityId = ViewModel.Model.Id
            };

            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskVerificationAcceptedByDesignDepartment,
                RecipientRole = Role.Constructor,
                RecipientUser = ViewModel.Model.UserConstructor,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        protected override void DoStepAction()
        {
            ViewModel.Statuses.Add(ScriptStep.VerificationAccept, $"Проверяющий: {this.Inspector}");
            ViewModel.Statuses.Add(ScriptStep.FinishByConstructor);
            ViewModel.SaveCommand.Execute();
            this.RaiseCanExecuteChanged();
        }

        protected override bool AllowDoStepAction()
        {
            var allow = ViewModel.Model.HasAnyUpdateStructureCostNumberTaskNotFinished == false;
            if (allow) return true;
            MessageService.Message("Отказ", "Сначала завершите задачи по изменению номеров стракчакостов.");
            return false;
        }

    }
}