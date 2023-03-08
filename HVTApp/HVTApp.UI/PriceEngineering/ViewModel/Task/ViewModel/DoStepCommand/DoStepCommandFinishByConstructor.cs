using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandFinishByConstructor : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.FinishByConstructor;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ����������?";

        public DoStepCommandFinishByConstructor(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
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

            if (vm.IsValidForProduction == false)
            {
                sb.AppendLine("\n��� ������������ ����������� ������������� ����������� �������.");
            }

            var step = needVerification
                ? ScriptStep.VerificationRequestByConstructor
                : ScriptStep.FinishByConstructor;
            vm.Statuses.Add(step);
            vm.Messenger.SendMessage(sb.ToString().TrimEnd('\n', '\r'));
            vm.SaveCommand.Execute();

            vm.AddAnswerFilesCommand.RaiseCanExecuteChanged();
            vm.RemoveAnswerFileCommand.RaiseCanExecuteChanged();

            if (needVerification)
            {
                EventAggregator.GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Publish(vm.Model);
            }
            else
            {
                EventAggregator.GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(vm.Model);
            }
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsTarget &&
                   ViewModel.IsEditMode &&
                   base.CanExecuteMethod();
        }
    }
}