using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStart : DoStepCommand<TaskViewModelBaseStartable>
    {
        protected override ScriptStep Step => ScriptStep.Start;
        protected override string ConfirmationMessage => "Вы уверены, что хотите запустить проработку?";

        public DoStepCommandStart(TaskViewModelBaseStartable viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            if(ViewModel.Model.UserConstructor != null)
                yield return new NotificationAboutPriceEngineeringTaskEventArg.StartConstructor(ViewModel.Model);
            else if (ViewModel.Model.DesignDepartment != null)
                yield return new NotificationAboutPriceEngineeringTaskEventArg.StartDesignDepartmentHead(ViewModel.Model);
        }

        protected override bool CanExecuteMethod()
        {
            return base.CanExecuteMethod() && ViewModel.IsChanged;
        }

        protected override string GetStatusComment()
        {
            if (this.ViewModel is TaskViewModelManagerNew)
                return null;

            var sb = new StringBuilder();
            if (this.ViewModel.FilesTechnicalRequirements.IsChanged)
            {
                sb.AppendLine("Изменения в Техническом Задании.");

                var actualFiles = this.ViewModel.FilesTechnicalRequirements
                    .Where(file => file.IsActual)
                    .OrderBy(file => file.CreationMoment)
                    .ToList();
                if (actualFiles.Any())
                {
                    sb.AppendLine("Актуальные файлы:");
                    actualFiles.ForEach(file => sb.AppendLine($" + {file.CreationMoment} {file.Name}"));
                }

                var notActualFiles = this.ViewModel.FilesTechnicalRequirements
                    .Where(file => file.IsActual == false)
                    .OrderBy(file => file.CreationMoment)
                    .ToList();
                if (notActualFiles.Any())
                {
                    sb.AppendLine("Не актуальные файлы:");
                    notActualFiles.ForEach(file => sb.AppendLine($" - {file.CreationMoment} {file.Name}"));
                }
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }

        protected override void BeforeDoStepAction()
        {
            if (this.ViewModel.UserConstructor?.IsActual == false)
            {
                this.ViewModel.Model.UserConstructor = null;
                MessageService.Message("Информация", "Исполнитель от ОГК удален из задачи, т.к. его профиль не актуален");
                this.ViewModel.Messenger.SendMessage("Исполнитель от ОГК удален из задачи, т.к. его профиль не актуален. Руководителю КБ необходимо назначить другого исполнителя.");
            }
        }
    }
}