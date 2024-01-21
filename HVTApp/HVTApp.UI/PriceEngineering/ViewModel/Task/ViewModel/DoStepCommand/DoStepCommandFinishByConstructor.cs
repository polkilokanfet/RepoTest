using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandFinishByConstructor : DoStepCommand<TaskViewModelConstructor>
    {
        protected override ScriptStep Step => ScriptStep.FinishByConstructor;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ����������?";

        public DoStepCommandFinishByConstructor(TaskViewModelConstructor viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var manager = ViewModel.Model.GetPriceEngineeringTasks(UnitOfWork).UserManager;

            if (this.ViewModel.Model.VerificationIsRequested)
            {
                yield return new NotificationArgsItem(ViewModel.Model.DesignDepartment.Head, Role.DesignDepartmentHead, $"��������� ���: {ViewModel.Model}");
                yield return new NotificationArgsItem(manager, Role.SalesManager, $"��� �� ��������: {ViewModel.Model}");
            }
            else
            {
                yield return new NotificationArgsItem(manager, Role.SalesManager, $"��� �����������: {ViewModel.Model}");
            }
        }

        protected override void DoStepAction()
        {
            if (ViewModel.Model.RequestForVerificationFromHead == false)
            {
                ViewModel.RequestForVerificationFromConstructor = MessageService.ConfirmationDialog("��������", "������ ��������� ���������� ����������?", defaultNo: true);
            }

            ViewModel.IsValidForProduction = MessageService.ConfirmationDialog("��������", "���������������� �� ���������� ��� ������������?", defaultNo: true);

            var step = ViewModel.Model.VerificationIsRequested
                ? ScriptStep.VerificationRequestByConstructor
                : ScriptStep.FinishByConstructor;
            ViewModel.Statuses.Add(step, GetStatusComment());
            ViewModel.SaveCommand.Execute();

            ViewModel.AddAnswerFilesCommand.RaiseCanExecuteChanged();
            ViewModel.RemoveAnswerFileCommand.RaiseCanExecuteChanged();
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