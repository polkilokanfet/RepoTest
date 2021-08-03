using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.ViewModel.Wrapper;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class AddStructureCostCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;

        public AddStructureCostCommand(PriceCalculationViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        protected override void ExecuteMethod()
        {
            var structureCost = new StructureCost { Comment = "No title" };
            var structureCostWrapper = new StructureCostWrapper(structureCost);
            ((PriceCalculationItem2Wrapper) _viewModel.SelectedItem).StructureCosts.Add(structureCostWrapper);
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedItem is PriceCalculationItem2Wrapper && !_viewModel.IsStarted;
        }
    }
}