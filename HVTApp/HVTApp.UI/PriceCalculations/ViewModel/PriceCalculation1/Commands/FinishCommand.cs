using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class FinishCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnityContainer _container;

        public FinishCommand(PriceCalculationViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ ��������� ������?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            _viewModel.PriceCalculationWrapper.TaskCloseMoment = DateTime.Now;
            _viewModel.SaveCommand.Execute();

            _viewModel.CanChangePriceOnPropertyChanged();

            _viewModel.SaveCommand.RaiseCanExecuteChanged();
            _container.Resolve<IEventAggregator>().GetEvent<AfterFinishPriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
        }

        protected override bool CanExecuteMethod()
        {
            if (_viewModel.PriceCalculationWrapper == null)
            {
                return false;
            }

            if (_viewModel.PriceCalculationWrapper.IsNeedExcelFile && !_viewModel.CalculationHasFile)
            {
                return false;
            }

            return _viewModel.IsStarted &&
                   !_viewModel.IsFinished &&
                   _viewModel.PriceCalculationWrapper.IsValid &&
                   _viewModel.PriceCalculationWrapper.PriceCalculationItems.SelectMany(item => item.StructureCosts).All(structureCost => structureCost.UnitPrice.HasValue);

        }
    }
}