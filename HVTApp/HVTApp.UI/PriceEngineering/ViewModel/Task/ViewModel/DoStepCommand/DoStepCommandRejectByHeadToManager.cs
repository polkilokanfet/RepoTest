using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToManager : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.RejectByHead;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи?";

        public DoStepCommandRejectByHeadToManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());
            yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"Руководитель КБ отклонил Вашу ТСП: {ViewModel.Model}");
        }

        protected override bool CanExecuteMethod()
        {
            return this.ViewModel.Model.UserConstructor == null && 
                   base.CanExecuteMethod();
        }
    }
}