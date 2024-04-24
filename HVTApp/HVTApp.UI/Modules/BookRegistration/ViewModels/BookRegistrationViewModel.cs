using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : DocumentLookupListViewModel
    {
        private readonly IFileManagerService _fileManagerService;
        private List<DocumentLookup> _documentLookups;
        private DocumentLookup _selectedDocumentLookup;
        private bool _showIncoming = true;
        private bool _showOutgoing = true;

        public DocumentLookup SelectedDocumentLookup
        {
            get => _selectedDocumentLookup;
            set
            {
                if (Equals(_selectedDocumentLookup, value))
                    return;

                _selectedDocumentLookup = value;
                EditDocumentCommand.RaiseCanExecuteChanged();
                OpenFolderCommand.RaiseCanExecuteChanged();
                PrintBlankLetterCommand.RaiseCanExecuteChanged();
                SelectedDocumentChanged?.Invoke(value?.Entity);
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
            ReloadCommand = new DelegateLogCommand(Load2);

            CreateOutgoingDocumentCommand = new DelegateLogCommand(() =>
            {
                var document = new Document
                {
                    Author = GlobalAppProperties.User.Employee,
                    SenderEmployee = GlobalAppProperties.Actual.SenderOfferEmployee
                };
                container.Resolve<IRegionManager>().RequestNavigateContentRegion<DocumentView>(new NavigationParameters { { DocumentDirection.Outgoing.ToString(), document } });
            });

            CreateIncomingDocumentCommand = new DelegateLogCommand(() =>
            {
                var document = new Document
                {
                    Author = GlobalAppProperties.User.Employee,
                    RecipientEmployee = GlobalAppProperties.Actual.SenderOfferEmployee
                };
                container.Resolve<IRegionManager>().RequestNavigateContentRegion<DocumentView>(new NavigationParameters { { DocumentDirection.Incoming.ToString(), document } });
            });

            EditDocumentCommand = new DelegateLogCommand(
                () =>
                {
                    var document = SelectedDocumentLookup.Entity;
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<DocumentView>(
                        new NavigationParameters
                        {
                            { document.Direction.ToString(), document },
                            { "edit", true }
                        });
                },
                () => SelectedDocumentLookup != null);

            OpenFolderCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().Message("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = _fileManagerService.GetPath(SelectedDocumentLookup.Entity);
                    Process.Start("explorer", $"\"{path}\"");
                },
                () => SelectedDocumentLookup != null);

            PrintBlankLetterCommand = new DelegateLogCommand(
                () =>
                {
                    var path = _fileManagerService.GetPath(SelectedDocumentLookup.Entity);
                    Container.Resolve<IPrintBlankLetterService>().PrintBlankLetter(SelectedDocumentLookup.Entity, path);
                },
                () => SelectedDocumentLookup != null && SelectedDocumentLookup.Entity.Direction == DocumentDirection.Outgoing);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDocumentEvent>().Subscribe(document =>
            {
                if(!_documentLookups.ContainsById(document))
                    _documentLookups.Insert(0, new DocumentLookup(document));
            });
        }

        public void Load2()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            
            var requests = UnitOfWork.Repository<IncomingRequest>().GetAll();
            List<Document> documents;
            if (GlobalAppProperties.UserIsManager)
            {
                documents = UnitOfWork.Repository<Document>().Find(document => document.Author.Id == GlobalAppProperties.User.Employee.Id);
                var requests2 = requests.Where(incomingRequest => incomingRequest.Performers.ContainsById(GlobalAppProperties.User.Employee));
                documents = documents.Union(requests2.Select(incomingRequest => incomingRequest.Document)).ToList();
            }
            else
            {
                documents = UnitOfWork.Repository<Document>().Find(document => 
                    document.Author.Id == GlobalAppProperties.User.Employee.Id ||
                    document.Direction == DocumentDirection.Incoming);
            }

            _documentLookups = documents
                .OrderByDescending(document => document.Date)
                .ThenByDescending(document => document.Number)
                .Select(document => new DocumentLookup(document, requests.SingleOrDefault(incomingRequest => incomingRequest.Document.Id == document.Id)?.Performers))
                .ToList();

            UpdateLookups();
        }

        public bool ShowIncoming
        {
            get => _showIncoming;
            set
            {
                if (Equals(_showIncoming, value)) return;
                _showIncoming = value;
                UpdateLookups();
            }
        }

        public bool ShowOutgoing
        {
            get => _showOutgoing;
            set
            {
                if(_showOutgoing == value) return;
                _showOutgoing = value;
                UpdateLookups();
            }
        }

        private void UpdateLookups()
        {
            var lookups = Lookups as ObservableCollection<DocumentLookup>;
            if (lookups == null) return;

            lookups.Clear();

            if (ShowIncoming && ShowOutgoing)
            {
                lookups.AddRange(_documentLookups);
                return;
            }

            if (ShowIncoming)
            {
                lookups.AddRange(
                    _documentLookups.Where(document => document.Direction == DocumentDirection.Incoming));
                return;
            }

            if (ShowOutgoing)
            {
                lookups.AddRange(
                    _documentLookups.Where(document => document.Direction == DocumentDirection.Outgoing));
            }
        }
    }
}
