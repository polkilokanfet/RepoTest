using System.Diagnostics;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using HVTApp.Model.Events;
using Prism.Commands;
using Prism.Events;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class LetterViewModel : LetterViewModelBase
    {
        public DelegateLogCommand OpenFileCommand { get; }
        public DelegateLogCommand LoadFileCommand { get; }
        public DelegateCommand SaveCommand { get; }

        /// <summary>
        /// редактирование
        /// </summary>
        /// <param name="container"></param>
        /// <param name="document"></param>
        public LetterViewModel(IUnityContainer container, Document document) : base(container, document)
        {
            var filesStorageService = container.Resolve<IFilesStorageService>();

            var lettersStoragePath = container.Resolve<IFileManagerService>().GetLettersDefaultStoragePath();
            
            SaveCommand = new DelegateCommand(() =>
                {
                    this.AcceptChanges();
                    this.UnitOfWork.SaveEntity(this.Model);
                    container.Resolve<IEventAggregator>().GetEvent<AfterSaveDocumentEvent>().Publish(this.Model);
                },
                () => this.IsValid && this.IsChanged);

            OpenFileCommand = new DelegateLogCommand(() =>
            {
                var fi = filesStorageService.FindFile(this.Model.Id, lettersStoragePath);
                Process.Start(fi.FullName);
            }, () => filesStorageService.FileContainsInStorage(this.Model.Id, lettersStoragePath));

            LoadFileCommand = new DelegateLogCommand(() =>
            {
                if (filesStorageService.FileContainsInStorage(this.Model.Id, lettersStoragePath))
                {
                    var dr = container.Resolve<IMessageService>().ConfirmationDialog("Заменить загруженный файл?");
                    if (dr == false) return;
                }

                var filePath = container.Resolve<IGetFilePaths>().GetFilePath();
                if (filePath == null) return;
                filesStorageService.LoadFile(lettersStoragePath, filePath, this.Model.Id.ToString(), true);
                OpenFileCommand.RaiseCanExecuteChanged();
            });

            this.PropertyChanged += (sender, args) => SaveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// создание
        /// </summary>
        /// <param name="container"></param>
        /// <param name="direction"></param>
        public LetterViewModel(IUnityContainer container, DocumentDirection direction) : this(container, new Document())
        {
            this.Model.WhoRegisteredUserId = GlobalAppProperties.User.Id;

            var employee = new EmployeeEmptyWrapper(UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.SenderOfferEmployee.Id));
            var author = new EmployeeEmptyWrapper(UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.User.Id));
            switch (direction)
            {
                case DocumentDirection.Incoming:
                {
                    this.Author = author;
                    this.RecipientEmployee = employee;
                    break;
                }
                case DocumentDirection.Outgoing:
                {
                    this.Author = author;
                    this.SenderEmployee = employee;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

    }
}