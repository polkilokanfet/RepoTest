using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class RejectCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnityContainer _container;

        public RejectCommand(PriceCalculationViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.HistoryItem.Comment))
            {
                _container.Resolve<IMessageService>().ShowOkMessageDialog("Внимание", "Для отклонения заполните комментарий");
                return;
            }

            var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите отклонить задачу?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            var historyItemWrapper = _viewModel.HistoryItem;
            historyItemWrapper.Moment = DateTime.Now;
            historyItemWrapper.Type = PriceCalculationHistoryItemType.Reject;
            _viewModel.PriceCalculationWrapper.History.Add(historyItemWrapper);

            _viewModel.SaveCommand.Execute();

            _viewModel.CanChangePriceOnPropertyChanged();

            _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
            _container.Resolve<IEventAggregator>().GetEvent<AfterRejectPriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
            _viewModel.RefreshCommands();
        }

        protected override bool CanExecuteMethod()
        {
            if (_viewModel.PriceCalculationWrapper == null)
            {
                return false;
            }

            return _viewModel.IsStarted &&
                   !_viewModel.IsFinished &&
                   !_viewModel.IsRejected &&
                   _viewModel.PriceCalculationWrapper.IsValid;
        }
    }
}