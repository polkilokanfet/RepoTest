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
    public class DoStepCommandFinishByConstructor : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.FinishByConstructor;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ����������?";

        public DoStepCommandFinishByConstructor(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());

            if (this.ViewModel.Model.RequestForVerificationFromHead ||
                this.ViewModel.Model.RequestForVerificationFromConstructor)
            {
                yield return new NotificationArgsItem(ViewModel.Model.DesignDepartment.Head, Role.DesignDepartmentHead, $"��������� ���: {ViewModel.Model}");
                yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"��� �� ��������: {ViewModel.Model}");
            }
            else
            {
                yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"��� �����������: {ViewModel.Model}");
            }
        }

        protected override void DoStepAction()
        {
            if (this.ViewModel is TaskViewModelConstructor == false)
                throw new ArgumentException();

            var vm = (TaskViewModelConstructor) ViewModel;

            if (vm.Model.RequestForVerificationFromHead == false)
            {
                var dr = MessageService.ShowYesNoMessageDialog("��������", "������ ��������� ���������� ����������?", defaultNo: true);
                vm.RequestForVerificationFromConstructor = dr == MessageDialogResult.Yes;
            }

            var dr1 = MessageService.ShowYesNoMessageDialog("��������", "���������������� �� ���������� ��� ������������?", defaultNo: true);
            vm.IsValidForProduction = dr1 == MessageDialogResult.Yes;


            var needVerification = vm.Model.RequestForVerificationFromHead || vm.RequestForVerificationFromConstructor;

            var step = needVerification
                ? ScriptStep.VerificationRequestByConstructor
                : ScriptStep.FinishByConstructor;
            vm.Statuses.Add(step, GetStatusComment());
            vm.SaveCommand.Execute();

            vm.AddAnswerFilesCommand.RaiseCanExecuteChanged();
            vm.RemoveAnswerFileCommand.RaiseCanExecuteChanged();
        }

        protected override string GetStatusComment()
        {
            var vm = (TaskViewModelConstructor)ViewModel;

            var sb = new StringBuilder()
                .AppendLine("���������� � ����������� ����������.")
                .AppendLine("�������� ����:")
                .AppendLine($" - {vm.ProductBlockEngineer.PrintToMessage()}");

            var pba = vm.ProductBlocksAdded.Where(blockAdded => blockAdded.Model.IsRemoved == false).ToList();
            if (pba.Any())
            {
                sb.AppendLine("����������� �����:");
                pba.ForEach(blockAdded => sb.AppendLine($" - {blockAdded}"));
            }

            var fa = vm.FilesAnswers.Where(x => x.IsActual).ToList();
            if (fa.Any())
            {
                sb.AppendLine("���������� ����������� ����� ���:");
                fa.ForEach(fileAnswer => sb.AppendLine($" - {fileAnswer}"));
            }

            sb.AppendLine(vm.IsValidForProduction
                ? "\n���������������� �� ���������� ��� ������������."
                : "\n���������������� �� ������������ ��� ������������.");

            sb.AppendLine(vm.Model.GetDesignDocumentationAvailabilityInfo());

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