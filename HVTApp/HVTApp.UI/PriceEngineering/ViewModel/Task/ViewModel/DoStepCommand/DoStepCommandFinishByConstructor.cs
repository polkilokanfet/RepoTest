using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandFinishByConstructor : DoStepCommand<TaskViewModelConstructor>
    {
        protected override ScriptStep Step => ViewModel.Model.VerificationIsRequested
            ? ScriptStep.VerificationRequestByConstructor
            : ScriptStep.FinishByConstructor;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ����������?";

        public DoStepCommandFinishByConstructor(TaskViewModelConstructor viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            if (this.ViewModel.Model.VerificationIsRequested)
            {
                yield return new NotificationAboutPriceEngineeringTaskEventArg.FinishByConstructorToDesignDepartmentHead(ViewModel.Model);
                yield return new NotificationAboutPriceEngineeringTaskEventArg.FinishByConstructorToManager1(ViewModel.Model, Manager);
            }
            else
            {
                yield return new NotificationAboutPriceEngineeringTaskEventArg.FinishByConstructorToManager2(ViewModel.Model, Manager);
            }
        }

        protected override void BeforeDoStepAction()
        {
            if (ViewModel.Model.RequestForVerificationFromHead == false)
                ViewModel.RequestForVerificationFromConstructor = MessageService.ConfirmationDialog("��������", "������ ��������� ���������� ����������?", defaultNo: true);

            ViewModel.IsValidForProduction = MessageService.ConfirmationDialog("��������", "���������������� �� ���������� ��� ������������?", defaultNo: true);
        }

        protected override string GetStatusComment()
        {
            var sb = new StringBuilder()
                .AppendLine("���������� � ����������� ����������.")
                .AppendLine("�������� ����:")
                .AppendLine($" - {ViewModel.ProductBlockEngineer.PrintToMessage()}");

            var pba = ViewModel.ProductBlocksAdded.Where(blockAdded => blockAdded.Model.IsRemoved == false).ToList();
            if (pba.Any())
            {
                sb.AppendLine("����������� �����:");
                pba.ForEach(blockAdded => sb.AppendLine($" - {blockAdded}"));
            }

            var fa = ViewModel.FilesAnswers.Where(x => x.IsActual).ToList();
            if (fa.Any())
            {
                sb.AppendLine("���������� ����������� ����� ���:");
                fa.ForEach(fileAnswer => sb.AppendLine($" - {fileAnswer}"));
            }

            sb.AppendLine(ViewModel.IsValidForProduction
                ? "\n���������������� �� ���������� ��� ������������."
                : "\n���������������� �� ������������ ��� ������������.");

            sb.AppendLine(ViewModel.Model.GetDesignDocumentationAvailabilityInfo());

            return sb.ToString().TrimEnd('\n', '\r');
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsTarget &&
                   ViewModel.IsEditMode &&
                   base.CanExecuteMethod();
        }
    }
}