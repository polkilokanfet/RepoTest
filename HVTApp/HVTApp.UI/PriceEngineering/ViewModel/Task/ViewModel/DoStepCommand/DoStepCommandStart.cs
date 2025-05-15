using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStart : DoStepCommand<TaskViewModelBaseStartable>
    {
        protected override ScriptStep Step => ScriptStep.Start;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ����������?";

        public DoStepCommandStart(TaskViewModelBaseStartable viewModel, IUnityContainer container) : base(viewModel, container)
        {
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
                sb.AppendLine("��������� � ����������� �������.");

                var actualFiles = this.ViewModel.FilesTechnicalRequirements
                    .Where(file => file.IsActual)
                    .OrderBy(file => file.CreationMoment)
                    .ToList();
                if (actualFiles.Any())
                {
                    sb.AppendLine("���������� �����:");
                    actualFiles.ForEach(file => sb.AppendLine($" + {file.CreationMoment} {file.Name}"));
                }

                var notActualFiles = this.ViewModel.FilesTechnicalRequirements
                    .Where(file => file.IsActual == false)
                    .OrderBy(file => file.CreationMoment)
                    .ToList();
                if (notActualFiles.Any())
                {
                    sb.AppendLine("�� ���������� �����:");
                    notActualFiles.ForEach(file => sb.AppendLine($" - {file.CreationMoment} {file.Name}"));
                }
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            if(ViewModel.Model.UserConstructor != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskStart,
                    RecipientRole = Role.Constructor,
                    RecipientUser = ViewModel.Model.UserConstructor,
                    TargetEntityId = ViewModel.Model.Id
                };
            }
            
            if (ViewModel.Model.DesignDepartment != null)
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskStart,
                    RecipientRole = Role.DesignDepartmentHead,
                    RecipientUser = ViewModel.Model.DesignDepartment.Head,
                    TargetEntityId = ViewModel.Model.Id
                };
            }
        }

        protected override void BeforeDoStepAction()
        {
            if (this.ViewModel.UserConstructor?.IsActual == false)
            {
                this.ViewModel.Model.UserConstructor = null;
                MessageService.Message("����������", "����������� �� ��� ������ �� ������, �.�. ��� ������� �� ��������");
                this.ViewModel.Messenger.SendMessage("����������� �� ��� ������ �� ������, �.�. ��� ������� �� ��������. ������������ �� ���������� ��������� ������� �����������.", true);
            }
        }
    }
}