using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : DocumentLookupListViewModel
    {
        private List<DocumentLookup> _documentLookups;
        private DocumentLookup _selectedDocumentLookup;
        private bool _showIncoming = true;
        private bool _showOutgoing = true;

        public DocumentLookup SelectedDocumentLookup
        {
            get { return _selectedDocumentLookup; }
            set
            {
                if (Equals(_selectedDocumentLookup, value))
                    return;

                _selectedDocumentLookup = value;
                ((DelegateCommand)EditDocumentCommand).RaiseCanExecuteChanged();
                ((DelegateCommand) OpenFolderCommand).RaiseCanExecuteChanged();
                SelectedDocumentChanged?.Invoke(value?.Entity);
            }
        }

        public event Action<Document> SelectedDocumentChanged;

        public ICommand CreateOutgoingDocumentCommand { get; }
        public ICommand CreateIncomingDocumentCommand { get; }
        public ICommand EditDocumentCommand { get; }
        public ICommand OpenFolderCommand { get; }
        public ICommand ReloadCommand { get; }

        public BookRegistrationViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load2);

            CreateOutgoingDocumentCommand = new DelegateCommand(() =>
            {
                var document = new Document
                {
                    Author = GlobalAppProperties.User.Employee,
                    SenderEmployee = GlobalAppProperties.Actual.SenderOfferEmployee
                };
                container.Resolve<IRegionManager>().RequestNavigateContentRegion<DocumentView>(new NavigationParameters { { DocumentDirection.Outgoing.ToString(), document } });
            });

            CreateIncomingDocumentCommand = new DelegateCommand(() =>
            {
                var document = new Document
                {
                    Author = GlobalAppProperties.User.Employee,
                    RecipientEmployee = GlobalAppProperties.Actual.SenderOfferEmployee
                };
                container.Resolve<IRegionManager>().RequestNavigateContentRegion<DocumentView>(new NavigationParameters { { DocumentDirection.Incoming.ToString(), document } });
            });

            EditDocumentCommand = new DelegateCommand(
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

            OpenFolderCommand = new DelegateCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = PathGetter.GetPath(SelectedDocumentLookup.Entity);
                    Process.Start("explorer", $"\"{path}\"");
                },
                () => SelectedDocumentLookup != null);

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
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                documents = UnitOfWork.Repository<Document>().Find(x => x.Author.Id == GlobalAppProperties.User.Employee.Id);
                var requests2 = requests.Where(x => x.Performers.ContainsById(GlobalAppProperties.User.Employee));
                documents = documents.Union(requests2.Select(x => x.Document)).ToList();
            }
            else
            {
                documents = UnitOfWork.Repository<Document>().GetAll();
            }

            _documentLookups = documents
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Number)
                .Select(x => new DocumentLookup(x, requests.SingleOrDefault(r => r.Document.Id == x.Id)?.Performers))
                .ToList();

            UpdateLookups();
        }

        public bool ShowIncoming
        {
            get { return _showIncoming; }
            set
            {
                if (Equals(_showIncoming, value)) return;
                _showIncoming = value;
                UpdateLookups();
            }
        }

        public bool ShowOutgoing
        {
            get { return _showOutgoing; }
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
            lookups.Clear();

            if (ShowIncoming && ShowOutgoing)
            {
                lookups.AddRange(_documentLookups);
                return;
            }


            if (ShowIncoming)
            {
                lookups.AddRange(_documentLookups.Where(x => x.Direction == DocumentDirection.Incoming));
                return;
            }


            if (ShowOutgoing)
            {
                lookups.AddRange(_documentLookups.Where(x => x.Direction == DocumentDirection.Outgoing));
            }
        }
    }
}
