using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public abstract class InvoiceForPaymentCommandBase : DelegateLogCommand
    {
        protected readonly TaskInvoiceForPayment TaskInvoiceForPayment;
        protected readonly IFilesStorageService FilesStorageService;
        protected readonly IMessageService MessageService;
        protected readonly string StorageDirectory;


        protected InvoiceForPaymentCommandBase(TaskInvoiceForPayment taskInvoiceForPayment, IFilesStorageService filesStorageService, IMessageService messageService)
        {
            TaskInvoiceForPayment = taskInvoiceForPayment;
            FilesStorageService = filesStorageService;
            MessageService = messageService;
            StorageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath;
        }

        public bool ContainsInStorage()
        {
            return FilesStorageService.FileContainsInStorage(TaskInvoiceForPayment.Id, StorageDirectory);
        }

    }
}