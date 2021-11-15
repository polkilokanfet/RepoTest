using System;
using System.IO;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RemoveFileAnswerCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public RemoveFileAnswerCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            //������
            var dr = MessageService.ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ ������� ���������� ����������?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            try
            {
                //��������
                FileInfo fileInfo = FilesStorageService.FindFile(ViewModel.SelectedAnswerFile.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath);
                File.Delete(fileInfo.FullName);
                UnitOfWork.Repository<AnswerFileTce>().Delete(ViewModel.SelectedAnswerFile.Model);
                ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles.Remove(ViewModel.SelectedAnswerFile);

                //����������
                ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();
                UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
            }

            ViewModel.SelectedAnswerFile = null;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted &&
                   !ViewModel.IsFinished &&
                   !ViewModel.IsRejected &&
                   !ViewModel.IsAccepted &&
                   ViewModel.SelectedAnswerFile != null;
        }
    }
}