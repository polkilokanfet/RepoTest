using System;
using System.IO;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
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
            var dr = MessageService.ConfirmationDialog("�������������", "�� �������, ��� ������ ������� ���������� ���?", defaultYes: true);
            if (dr == false) return;

            try
            {
                //��������
                FileInfo fileInfo = FilesStorageService.FindFile(ViewModel.SelectedShippingCalculationFile.Id, GlobalAppProperties.Actual.ShippingCostFilesPath);
                File.Delete(fileInfo.FullName);
                UnitOfWork.Repository<ShippingCostFile>().Delete(ViewModel.SelectedShippingCalculationFile.Model);
                ViewModel.TechnicalRequrementsTaskWrapper.ShippingCostFiles.Remove(ViewModel.SelectedShippingCalculationFile);

                //����������
                ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();
                UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                MessageService.Message("Exception", e.PrintAllExceptions());
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