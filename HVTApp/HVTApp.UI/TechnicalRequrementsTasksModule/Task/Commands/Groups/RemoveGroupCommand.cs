using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RemoveGroupCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        
        public RemoveGroupCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var dr = MessageService.ConfirmationDialog("��������", "������������� ������ ������� �� ������ ��� ������������?", defaultNo: true);
            if (dr == false) return;

            var selectedGroup = (TechnicalRequrements2Wrapper)ViewModel.SelectedItem;

            var salesUnits = selectedGroup.SalesUnits.ToList();

            //�������, ������ ������ ������� �� �������, �.�. ��� ��������� � ������������
            var salesUnitsNotForRemove = salesUnits
                .Where(salesUnit => salesUnit.Model.SignalToStartProduction.HasValue)
                .Where(salesUnit => salesUnit.Model.ActualPriceCalculationItem(UnitOfWork)?.Id == selectedGroup.Model.Id)
                .ToList();

            if (salesUnitsNotForRemove.Any())
            {
                MessageService.Message("��������", "�� �� ������ ������� ��������� ������, �.�. ��� ��������� � ������������.");

                var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                salesUnitsToRemove.ForEach(salesUnit => selectedGroup.SalesUnits.Remove(salesUnit));
                if (!selectedGroup.SalesUnits.Any())
                    ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Remove(selectedGroup);
            }
            else
            {
                ViewModel.TechnicalRequrementsTaskWrapper.Requrements.Remove(selectedGroup);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.WasStarted && 
                   !ViewModel.IsStarted &&
                   ViewModel.SelectedItem is TechnicalRequrements2Wrapper;
        }
    }
}