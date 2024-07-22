using System;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public class OpenInvoiceForPaymentCommand : InvoiceForPaymentCommandBase
    {
        public OpenInvoiceForPaymentCommand(TaskInvoiceForPayment taskInvoiceForPayment, IFilesStorageService filesStorageService, IMessageService messageService) : base(taskInvoiceForPayment, filesStorageService, messageService)
        {
        }

        protected override void ExecuteMethod()
        {
            if (this.ContainsInStorage() == false)
            {
                MessageService.Message("Уведомление", "Счёт не загружен в хранилище");
                return;
            }

            FilesStorageService.OpenFileFromStorage(TaskInvoiceForPayment.Id, StorageDirectory, addToFileName: $" ");
        }
    }
}