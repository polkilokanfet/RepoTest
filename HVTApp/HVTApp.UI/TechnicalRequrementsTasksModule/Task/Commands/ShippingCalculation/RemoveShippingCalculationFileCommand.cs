using System;
using System.IO;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RemoveShippingCalculationFileCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public RemoveShippingCalculationFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            //������
            var dr = MessageService.ShowYesNoMessageDialog("�������������", "�� �������, ��� ������ ������� ���������� ���?", defaultYes: true);
            if (dr != MessageDialogResult.Yes) return;

            try
            {
                //��������
                FileInfo fileInfo = FilesStorage.FindFile(ViewModel.SelectedShippingCalculationFile.Id, GlobalAppProperties.Actual.ShippingCostFilesPath);
                File.Delete(fileInfo.FullName);
                UnitOfWork.Repository<ShippingCostFile>().Delete(ViewModel.SelectedShippingCalculationFile.Model);
                ViewModel.TechnicalRequrementsTaskWrapper.ShippingCostFiles.Remove(ViewModel.SelectedShippingCalculationFile);

                //����������
                ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();
                UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                MessageService.ShowOkMessageDialog("Exception", e.PrintAllExceptions());
            }

            ViewModel.SelectedShippingCalculationFile = null;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedShippingCalculationFile != null && 
                   ViewModel.IsStarted &&
                   !ViewModel.IsAccepted &&
                   !ViewModel.IsFinished &&
                   !ViewModel.IsRejected;
        }
    }
}