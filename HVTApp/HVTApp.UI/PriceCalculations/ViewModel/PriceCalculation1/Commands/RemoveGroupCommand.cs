using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RemoveGroupCommand : BasePriceCalculationCommand
    {
        public RemoveGroupCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var result = MessageService.ConfirmationDialog("��������", "������������� ������ ������� �� ������� ������ ������������?", defaultNo: true);
            if (result == false) return;

            var groupToRemove = (PriceCalculationItem2Wrapper)ViewModel.SelectedItem;

            var salesUnits = groupToRemove.SalesUnits.ToList();

            //�������, ������ ������ ������� �� �������, �.�. ��� ��������� � ������������
            var salesUnitsNotForRemove = salesUnits
                .Where(salesUnit => salesUnit.Model.SignalToStartProduction.HasValue)
                .Where(salesUnit => salesUnit.Model.ActualPriceCalculationItem(ViewModel.UnitOfWork1)?.Id == groupToRemove.Model.Id)
                .ToList();

            //���� ���� ��, ��� ������ �������
            if (salesUnitsNotForRemove.Any())
            {
                MessageService.Message("��������", "�� �� ������ ������� ��������� ������, �.�. ��� ��������� � ������������.");

                var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                salesUnitsToRemove.ForEach(salesUnit => groupToRemove.SalesUnits.Remove(salesUnit));
                if (groupToRemove.SalesUnits.Any() == false)
                {
                    ViewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(groupToRemove);
                }
            }
            else
            {
                ViewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(groupToRemove);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedItem is PriceCalculationItem2Wrapper && !ViewModel.IsStarted;
        }
    }
}