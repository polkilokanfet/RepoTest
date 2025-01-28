using System;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.TaskInvoiceForPayment1.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForBackManager
{
    public class LoadInvoiceForPaymentCommand : InvoiceForPaymentCommandBase
    {
        private readonly Func<bool> _canExecute;

        public LoadInvoiceForPaymentCommand(TaskInvoiceForPayment taskInvoiceForPayment, IFilesStorageService filesStorageService, IMessageService messageService, Func<bool> canExecute) : base(taskInvoiceForPayment, filesStorageService, messageService)
        {
            _canExecute = canExecute;
        }

        protected override void ExecuteMethod()
        {
            try
            {
                if (this.ContainsInStorage())
                {
                    var dr = MessageService.ConfirmationDialog("���� ��� �������� � ���������. ��������?");
                    if (dr == false) return;
                }
            }
            catch (FileNotSingleFoundException e)
            {
                var dr = MessageService.ConfirmationDialog("��� ��������� ����� ������ �����. ���������� �� �������. �������?");
                if (dr == false) return;
                FilesStorageService.RemoveFiles(StorageDirectory, TaskInvoiceForPayment.Id);
            }

            FilesStorageService.LoadFileToStorage(StorageDirectory, TaskInvoiceForPayment.Id, true);
        }

        protected override bool CanExecuteMethod()
        {
            return _canExecute?.Invoke() ?? base.CanExecuteMethod();
        }
    }
}