using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RemoveGroupCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private IMessageService _messageService;

        public RemoveGroupCommand(PriceCalculationViewModel viewModel, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _viewModel = viewModel;
            _container = container;
            _unitOfWork = unitOfWork;
            _messageService = container.Resolve<IMessageService>();
        }

        protected override void ExecuteMethod()
        {
            var result = _messageService.ShowYesNoMessageDialog("”даление", "ƒействительно хотите удалить из расчета группу оборудовани€?", defaultNo: true);
            if (result != MessageDialogResult.Yes) return;

            var selectedGroup = (PriceCalculationItem2Wrapper)_viewModel.SelectedItem;

            var salesUnits = selectedGroup.SalesUnits.ToList();

            //единицы, которы нельз€ удалить из расчета, т.к. они размещены в производстве
            var salesUnitsNotForRemove = salesUnits
                .Where(x => x.Model.SignalToStartProduction.HasValue)
                .Where(x => x.Model.ActualPriceCalculationItem(_unitOfWork)?.Id == selectedGroup.Model.Id)
                .ToList();

            if (salesUnitsNotForRemove.Any())
            {
                _messageService.ShowOkMessageDialog("”даление", "¬ы не можете удалить некоторые строки, т.к. они размещены в производстве.");

                var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                salesUnitsToRemove.ForEach(x => selectedGroup.SalesUnits.Remove(x));
                if (!selectedGroup.SalesUnits.Any())
                    _viewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
            }
            else
            {
                _viewModel.PriceCalculationWrapper.PriceCalculationItems.Remove(selectedGroup);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedItem is PriceCalculationItem2Wrapper && !_viewModel.IsStarted;
        }
    }
}