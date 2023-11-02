using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class MergeCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public MergeCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var result = MessageService.ConfirmationDialog("�������", "������������� ������ ����� ������, ���������� ������?", defaultYes: true);
            if (result == false) return;

            //������ ��� �������
            var items = ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Where(x => x.IsChecked).ToList();

            if (items.Select(x => x.SalesUnit.Facility.Id).Distinct().Count() > 1)
            {
                MessageService.Message("�������", "�� �� ������ ���������� ������ � ������� ��������� ��������.");
                return;
            }

            if (items.Select(x => x.SalesUnit.Product.Id).Distinct().Count() > 1)
            {
                MessageService.Message("�������", "�� �� ������ ���������� ������ � ������� ���������� ��������.");
                return;
            }

            if (items.Select(x => x.SalesUnit.OrderInTakeDate).Distinct().Count() > 1)
            {
                MessageService.Message("�������", "�� �� ������ ���������� ������ � ������� ������ ���.");
                return;
            }

            if (items.Select(x => x.SalesUnit.RealizationDateCalculated).Distinct().Count() > 1)
            {
                MessageService.Message("�������", "�� �� ������ ���������� ������ � ������� ������ ����������.");
                return;
            }

            if (items.Select(x => x.SalesUnit.Producer).Distinct().Count() > 1)
            {
                MessageService.Message("�������", "�� �� ������ ���������� ������ � ������� ���������������.");
                return;
            }

            var itemToSave = items.First();
            items.Remove(itemToSave);

            foreach (var item in items)
            {
                item.SalesUnits.ForEach(salesUnit => itemToSave.SalesUnits.Add(salesUnit));
                ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Remove(item);
                if (UnitOfWork.Repository<TechnicalRequrements>().GetById(item.Model.Id) != null)
                    UnitOfWork.Repository<TechnicalRequrements>().Delete(item.Model);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted &&
                   ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Count(requrements => requrements.IsChecked) > 1;
        }
    }
}