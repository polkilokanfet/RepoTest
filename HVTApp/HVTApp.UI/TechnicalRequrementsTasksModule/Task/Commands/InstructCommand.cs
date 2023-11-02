using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class InstructCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public InstructCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (ViewModel.TechnicalRequrementsTaskWrapper.Model.BackManager != null)
            {
                var dr = MessageService.ConfirmationDialog("Информация", "Back manager уже назначен. Вы хотите его сменить?");
                if (dr == false) return;
            }

            var backManagers = UnitOfWork.Repository<User>().Find(user => user.IsActual && user.Roles.Any(role => role.Role == Role.BackManager));
            var selectService = Container.Resolve<ISelectService>();
            var backManager = selectService.SelectItem(backManagers, ViewModel.TechnicalRequrementsTaskWrapper.Model.BackManager?.Id);

            if (backManager != null)
            {
                ViewModel.TechnicalRequrementsTaskWrapper.BackManager = new UserWrapper(backManager);

                ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Instruct;
                ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
                var comment = ViewModel.HistoryElementWrapper.Comment;
                ViewModel.HistoryElementWrapper.Comment = $"Назначен БМ: {backManager.Employee.Person}";
                if (!string.IsNullOrWhiteSpace(comment))
                {
                    ViewModel.HistoryElementWrapper.Comment = $"{ViewModel.HistoryElementWrapper.Comment}. Комментарий: {comment}.";
                }
                ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

                ViewModel.SaveCommand.Execute();

                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);
                Container.Resolve<IEventAggregator>().GetEvent<AfterInstructTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

                ViewModel.SetNewHistoryElement();
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid && ViewModel.CurrentUserIsBackManagerBoss;
        }
    }
}