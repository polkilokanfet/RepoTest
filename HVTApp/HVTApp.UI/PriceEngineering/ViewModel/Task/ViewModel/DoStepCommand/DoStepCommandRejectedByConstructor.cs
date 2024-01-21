using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByConstructor : DoStepCommand<TaskViewModelConstructor>
    {
        protected override ScriptStep Step => ScriptStep.RejectByConstructor;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ���������� ������?";

        public DoStepCommandRejectedByConstructor(TaskViewModelConstructor viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            yield return new NotificationArgsItem(ViewModel.Model.GetPriceEngineeringTasks(UnitOfWork).UserManager, Role.SalesManager, $"����������� �������� ���� ���: {ViewModel.Model}");
        }

        protected override void BeforeDoStepAction()
        {
            ViewModel.RejectChanges();
        }

        protected override bool CanExecuteMethod()
        {
            return Step.AllowDoStep(ViewModel.Status);
        }
    }
}