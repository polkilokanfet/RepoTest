using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class SaveCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnityContainer _container;

        public SaveCommand(PriceCalculationViewModel viewModel, IUnitOfWork unitOfWork, IUnityContainer container)
        {
            _viewModel = viewModel;
            _unitOfWork = unitOfWork;
            _container = container;
        }

        protected override void ExecuteMethod()
        {

            var priceCalculation = _unitOfWork.Repository<PriceCalculation>().GetById(_viewModel.PriceCalculationWrapper.Model.Id);
            if (priceCalculation == null)
            {
                _unitOfWork.Repository<PriceCalculation>().Add(_viewModel.PriceCalculationWrapper.Model);
            }

            //если есть удаленные группы
            foreach (var removedItem in _viewModel.PriceCalculationWrapper.PriceCalculationItems.RemovedItems)
            {
                foreach (var structureCost in removedItem.Model.StructureCosts.ToList())
                {
                    _unitOfWork.Repository<StructureCost>().Delete(structureCost);
                }
                removedItem.Model.SalesUnits.Clear();
                _unitOfWork.Repository<PriceCalculationItem>().Delete(removedItem.Model);
            }

            if (_unitOfWork.SaveChanges().OperationCompletedSuccessfully)
            {
                _viewModel.PriceCalculationWrapper.AcceptChanges();
                _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
            }

            this.RaiseCanExecuteChanged();
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.PriceCalculationWrapper.IsValid && _viewModel.PriceCalculationWrapper.IsChanged;
        }
    }
}