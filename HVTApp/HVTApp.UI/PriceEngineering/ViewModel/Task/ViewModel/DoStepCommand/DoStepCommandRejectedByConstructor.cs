using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByConstructor : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.RejectByConstructor;
        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи?";

        public DoStepCommandRejectedByConstructor(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());
            yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"Исполнитель отклонил Ваше ТСП: {ViewModel.Model}");
        }

        protected override void DoStepAction()
        {
            ViewModel.RejectChanges();
            base.DoStepAction();
        }

        protected override bool CanExecuteMethod()
        {
            return Step.AllowDoStep(ViewModel.Status);
        }
    }
}