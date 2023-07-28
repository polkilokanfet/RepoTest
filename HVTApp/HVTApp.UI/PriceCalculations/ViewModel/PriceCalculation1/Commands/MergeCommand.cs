using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class MergeCommand : BasePriceCalculationCommand
    {
        public MergeCommand(PriceCalculationViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var result = MessageService.ShowYesNoMessageDialog("�������", "������������� ������ ����� ������, ���������� ������?", defaultYes: true);
            if (result != MessageDialogResult.Yes) return;

            //������ ��� �������
            var items = ViewModel.PriceCalculationWrapper.PriceCalculationItems.Where(x => x.IsChecked).ToList();

            if (items.Select(x => x.Facility.Id).Distinct().Count() > 1)
            {
                MessageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ��������� ��������.");
                return;
            }

            if (items.Select(x => x.Product.Id).Distinct().Count() > 1)
            {
                MessageService.ShowOkMessageDialog("�������", "�� �� ������ ���������� ������ � ������� ���������� ��������.");
                return;
            }

            var itemToSave = items.First();
            items.Remove(itemToSave);

            foreach (var item in items)
            {
                item.SalesUnits.ForEach(x => itemToSave.SalesUnits.Add(x));
                ViewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(item);
                if (ViewModel.UnitOfWork1.Repository<PriceCalculationItem>().GetById(item.Model.Id) != null)
                    ViewModel.UnitOfWork1.Repository<PriceCalculationItem>().Delete(item.Model);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted && ViewModel.PriceCalculationWrapper.PriceCalculationItems.Count(x => x.IsChecked) > 1;
        }
    }
}