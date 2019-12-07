using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : DocumentLookupListViewModel
    {
        public ICommand CreateOutgoingDocumentCommand { get; }
        public ICommand CreateIncomingDocumentCommand { get; }

        public BookRegistrationViewModel(IUnityContainer container) : base(container)
        {
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
        }
    }
}
