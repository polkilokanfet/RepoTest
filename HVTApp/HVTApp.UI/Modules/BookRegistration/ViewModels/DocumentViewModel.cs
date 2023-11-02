using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class DocumentViewModel : DocumentDetailsViewModel
    {
        private readonly IFileManagerService _fileManagerService;

        public DocumentDirection Direction { get; private set; } = DocumentDirection.Outgoing;

        public string DocType => Direction == DocumentDirection.Outgoing
            ? "Исходящий документ"
            : "Входящий документ";

        public DelegateLogCommand OpenFolderCommand { get; }
        public DelegateLogCommand AddFilesCommand { get; }

        public DocumentViewModel(IUnityContainer container) : base(container)
        {
            _fileManagerService = container.Resolve<IFileManagerService>();

            OpenFolderCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().Message("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = _fileManagerService.GetPath(Item.Model);
                    Process.Start("explorer", $"\"{path}\"");
                });

            AddFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var fileNames = container.Resolve<IGetFilePaths>().GetFilePaths();

                    var rootDirectoryPath = _fileManagerService.GetPath(Item.Model);
                    foreach (var fileName in fileNames)
                    {
                        File.Copy(fileName, $"{rootDirectoryPath}\\{Path.GetFileName(fileName)}");
                    }
                });
        }

        /// <summary>
        /// Загрузка при создании нового документа
        /// </summary>
        /// <param name="document"></param>
        /// <param name="direction"></param>
        public void LoadCreate(Document document, string direction)
        {
            this.Load(new Document());

            if(Equals(direction, DocumentDirection.Incoming.ToString()))
                Direction = DocumentDirection.Incoming;

            if (document.Author != null)
                Item.Author = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.Author.Id));

            if (document.SenderEmployee != null)
                Item.SenderEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.SenderEmployee.Id));

            if (document.RecipientEmployee != null)
                Item.RecipientEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(document.RecipientEmployee.Id));

            RaisePropertyChanged(nameof(DocType));
        }

        public void LoadEdit(Document document)
        {
            this.Load(document);
            RaisePropertyChanged(nameof(DocType));
        }

        protected override void SaveItem()
        {
            base.SaveItem();
            //if (Item.Model.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany?.Id)
            //    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveIncomingDocumentSyncEvent>().Publish(Item.Model);
        }

    }
}