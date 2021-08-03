using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RemoveStructureCostCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveStructureCostCommand(PriceCalculationViewModel viewModel, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _viewModel = viewModel;
            _container = container;
            _unitOfWork = unitOfWork;
        }

        protected override void ExecuteMethod()
        {
            var result = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить StructureCost?", defaultNo: true);
            if (result != MessageDialogResult.Yes) return;

            var structureCost = (StructureCostWrapper)_viewModel.SelectedItem;
            var calculationItem2Wrapper = _viewModel.PriceCalculationWrapper.PriceCalculationItems.Single(x => x.StructureCosts.Contains(structureCost));
            calculationItem2Wrapper.StructureCosts.Remove(structureCost);
            if (_unitOfWork.Repository<StructureCost>().GetById(structureCost.Id) != null)
            {
                _unitOfWork.Repository<StructureCost>().Delete(structureCost.Model);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedItem is StructureCostWrapper && !_viewModel.IsStarted;
        }
    }
}