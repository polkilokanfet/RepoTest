using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptedByManager : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.Accept;
        protected override string ConfirmationMessage => "¬ы уверены, что хотите прин€ть проработку задачи?";

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = EventServiceActionType.PriceEngineeringTaskAccept,
                RecipientRole = Role.Constructor,
                RecipientUser = ViewModel.Model.UserConstructor,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        public DoStepCommandAcceptedByManager(TaskViewModelManagerOld viewModel, IUnityContainer container, Action doAfterAction) : base(viewModel, container, doAfterAction)
        {
        }
    }
}