using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestsViewModel : ViewModelBase
    {
        public ObservableCollection<IncomingRequestLookup> IncomingRequests { get; } = new ObservableCollection<IncomingRequestLookup>();

        public ICommand ReloadCommand { get; set; }

        public IncomingRequestsViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //все сохраненные входящие запросы
            var requests = UnitOfWork.Repository<IncomingRequest>().GetAll();
            //все входящие документы
            var incomingDocuments = UnitOfWork.Repository<Document>().Find(x => x.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany.Id);

            //непорученные никому входящие документы
            var targetDocuments = incomingDocuments.Except(requests.Select(x => x.Document)).ToList();
            var targetRequests = targetDocuments.Select(x => new IncomingRequest {Document = x});

            //обновляем коллекцию
            IncomingRequests.Clear();
            IncomingRequests.AddRange(requests.Union(targetRequests).OrderBy(x => x.Document.Date).Select(x => new IncomingRequestLookup(x)));
        }
    }
}