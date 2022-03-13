using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class StartCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public StartCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (MessageService.ShowYesNoMessageDialog("Запуск задачи в ТСЕ", "Вы уверены, что хотите запустить задачу?") != MessageDialogResult.Yes)
                return;

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Start;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            //удаление бэк менеджера, если он уже не работает в компании
            if (ViewModel.TechnicalRequrementsTaskWrapper.BackManager != null &&
                ViewModel.TechnicalRequrementsTaskWrapper.BackManager.IsActual == false)
            {
                ViewModel.TechnicalRequrementsTaskWrapper.BackManager = null;
            }

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterStartTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.SetNewHistoryElement();
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted == false && 
                   ViewModel.IsValid && 
                   ViewModel.IsChanged;
        }
    }
}