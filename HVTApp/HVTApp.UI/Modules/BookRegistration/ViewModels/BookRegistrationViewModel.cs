using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : DocumentLookupListViewModel
    {
        private DocumentLookup _selectedDocumentLookup;
        public DocumentLookup SelectedDocumentLookup
        {
            get { return _selectedDocumentLookup; }
            set
            {
                _selectedDocumentLookup = value;
                ((DelegateCommand) OpenFolderCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand CreateOutgoingDocumentCommand { get; }
        public ICommand CreateIncomingDocumentCommand { get; }
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
        }

        public void Load2()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            List<Document> documents;

            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                documents = UnitOfWork.Repository<Document>().Find(x => x.Author.Id == GlobalAppProperties.User.Employee.Id);
                var requests = UnitOfWork.Repository<IncomingRequest>().Find(x => x.Performers.ContainsById(GlobalAppProperties.User.Employee));
                documents = documents.Union(requests.Select(x => x.Document)).ToList();
            }
            else
            {
                documents = UnitOfWork.Repository<Document>().GetAll();
            }

            (Lookups as ObservableCollection<DocumentLookup>).Clear();
            (Lookups as ObservableCollection<DocumentLookup>).AddRange(documents.OrderByDescending(x => x.Date).ThenBy(x => x.Number).Select(x => new DocumentLookup(x)));
        }
    }
}
