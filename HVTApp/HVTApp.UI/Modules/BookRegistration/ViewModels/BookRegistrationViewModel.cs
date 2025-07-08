using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : ViewModelBaseCanExportToExcel
    {
        private readonly IFileManagerService _fileManagerService;
        private Letter _selectedLetter;

        public ObservableCollection<Letter> Letters { get; } = new ObservableCollection<Letter>();

        public Letter SelectedLetter
        {
            get => _selectedLetter;
            set
            {
                SetProperty(ref _selectedLetter, value, () =>
                {
                    EditDocumentCommand.RaiseCanExecuteChanged();
                    OpenFolderCommand.RaiseCanExecuteChanged();
                    PrintBlankLetterCommand.RaiseCanExecuteChanged();
                    SelectedDocumentChanged?.Invoke(value?.Entity);
                });
            }
        }

        public event Action<Document> SelectedDocumentChanged;

        #region Commands

        /// <summary>
        /// Создание исходящего документа
        /// </summary>
        public DelegateLogCommand CreateOutgoingDocumentCommand { get; }

        /// <summary>
        /// Создание входящего документа
        /// </summary>
        public DelegateLogCommand CreateIncomingDocumentCommand { get; }

        /// <summary>
        /// Редактирование документа
        /// </summary>
        public DelegateLogCommand EditDocumentCommand { get; }
        public DelegateLogCommand OpenFolderCommand { get; }
        public DelegateLogCommand ReloadCommand { get; }

        /// <summary>
        /// Печать бланка письма
        /// </summary>
        public DelegateLogCommand PrintBlankLetterCommand { get; }
        
        #endregion

        public BookRegistrationViewModel(IUnityContainer container) : base(container)
        {
            _fileManagerService = container.Resolve<IFileManagerService>();
            ReloadCommand = new DelegateLogCommand(Load);

            CreateOutgoingDocumentCommand = new DelegateLogCommand(() =>
            {
                var documentViewModel = new LetterViewModel(container, DocumentDirection.Outgoing);
                container.Resolve<IDialogService>().Show(documentViewModel, "Регистрация исходящего письма");
            });

            CreateIncomingDocumentCommand = new DelegateLogCommand(() =>
            {
                var documentViewModel = new LetterViewModel(container, DocumentDirection.Incoming);
                container.Resolve<IDialogService>().Show(documentViewModel, "Регистрация входящего письма");
            });

            EditDocumentCommand = new DelegateLogCommand(
                () =>
                {
                    var document = SelectedLetter.Entity;
                    var documentViewModel = new LetterViewModel(container, document);
                    container.Resolve<IDialogService>().Show(documentViewModel, $"Редактирование письма №{document.RegNumber}");
                },
                () => SelectedLetter != null);

            OpenFolderCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().Message("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = _fileManagerService.GetPath(SelectedLetter.Entity);
                    Process.Start("explorer", $"\"{path}\"");
                },
                () => SelectedLetter != null);

            PrintBlankLetterCommand = new DelegateLogCommand(
                () =>
                {
                    var path = _fileManagerService.GetPath(SelectedLetter.Entity);
                    Container.Resolve<IPrintBlankLetterService>().PrintBlankLetter(SelectedLetter.Entity, path);
                },
                () => SelectedLetter != null && SelectedLetter.Entity.Direction == DocumentDirection.Outgoing);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDocumentEvent>().Subscribe(document =>
            {
                if(Letters.ContainsById(document))
                    Letters.Single(letter => letter.Entity.Id == document.Id).Refresh(document);
                else
                    Letters.Insert(0, new Letter(document));
            });

            this.Load();
        }

        public void Load()
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var letters = unitOfWork.Repository<Document>()
                    .GetAllAsNoTracking()
                    .OrderByDescending(document => document.Date)
                    .ThenByDescending(document => document.Number)
                    .Select(document => new Letter(document))
                    .ToList();

            Letters.Clear();
            Letters.AddRange(letters);
        }
    }
}
