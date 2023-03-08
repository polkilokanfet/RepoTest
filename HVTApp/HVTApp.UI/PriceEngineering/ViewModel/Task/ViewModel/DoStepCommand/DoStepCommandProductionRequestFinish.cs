using System.Linq;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestFinish : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "�� �������, ��� ������� ������������?";

        public DoStepCommandProductionRequestFinish(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            //�������� �� �������� �/�
            if (ViewModel.Model.SalesUnits.Any(x => x.Order != null))
            {
                MessageService.ShowOkMessageDialog("�����", "� ������ ������������ ��� ���� �������� ������.");
                return;
            }

            var order = new Order();
            if (this.Container.Resolve<IUpdateDetailsService>().UpdateDetails(order) == false)
                return;

            foreach (var salesUnit in ViewModel.Model.SalesUnits)
            {
                salesUnit.Order = order;
            }

            base.DoStepAction();
        }
    }
}