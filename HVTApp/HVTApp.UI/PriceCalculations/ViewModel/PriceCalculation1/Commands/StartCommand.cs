using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public class StartCommand : DelegateLogCommand
    {
        private readonly PriceCalculationViewModel _viewModel;
        private readonly IUnityContainer _container;

        public StartCommand(PriceCalculationViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
        }

        protected override void ExecuteMethod()
        {
            var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ ���������� ������?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;
            _viewModel.PriceCalculationWrapper.TaskOpenMoment = DateTime.Now;
            _viewModel.SaveCommand.Execute();
            _container.Resolve<IEventAggregator>().GetEvent<AfterStartPriceCalculationEvent>().Publish(_viewModel.PriceCalculationWrapper.Model);
            _viewModel.RefreshCommands();
        }

        protected override bool CanExecuteMethod()
        {
            return !_viewModel.IsStarted && 
                   _viewModel.PriceCalculationWrapper.IsValid && 
                   GlobalAppProperties.User.Id == _viewModel.PriceCalculationWrapper.Initiator?.Id;
        }
    }
}