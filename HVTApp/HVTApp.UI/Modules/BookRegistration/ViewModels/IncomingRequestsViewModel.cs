using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.BookRegistration.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestsViewModel : ViewModelBaseCanExportToExcel
    {
        private IncomingRequestLookup _selectedIncomingRequest;

        public bool IsDirectorView => GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director;
        public bool IsManagerView => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;

        public ObservableCollection<IncomingRequestLookup> IncomingRequests { get; } = new ObservableCollection<IncomingRequestLookup>();

        public IncomingRequestLookup SelectedIncomingRequest
        {
            get { return _selectedIncomingRequest; }
            set
            {
                _selectedIncomingRequest = value;
                ((DelegateCommand)InstructRequestCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)OpenFolderCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RequestIsDoneCommand).RaiseCanExecuteChanged();
            }
        }

        //перезагрузить
        public ICommand ReloadCommand { get; }       
        //поручить запрос
        public ICommand InstructRequestCommand { get; }

        public ICommand OpenFolderCommand { get; }

        public ICommand RequestIsDoneCommand { get; }

        public IncomingRequestsViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);

            InstructRequestCommand = new DelegateCommand(
                () =>
                {
                    container.Resolve<IRegionManager>()
                        .RequestNavigateContentRegion<IncomingRequestView>(
                            new NavigationParameters
                            {
                                {"Model", SelectedIncomingRequest.Entity},
                                {"UnitOfWork", UnitOfWork}
                            });
                },
                () => SelectedIncomingRequest != null);

            OpenFolderCommand = new DelegateCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = PathGetter.GetPath(SelectedIncomingRequest.Entity.Document);
                    Process.Start("explorer", $"\"{path}\"");
                },
                () => SelectedIncomingRequest != null);

            RequestIsDoneCommand = new DelegateCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var request = unitOfWork.Repository<IncomingRequest>().GetById(SelectedIncomingRequest.Id);
                    request.DoneDate = DateTime.Now;
                    unitOfWork.SaveChanges();

                    SelectedIncomingRequest.Refresh(request);
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveIncomingRequestEvent>().Publish(request);
                    ((DelegateCommand)RequestIsDoneCommand).RaiseCanExecuteChanged();
                },
                () => SelectedIncomingRequest != null && !SelectedIncomingRequest.Entity.IsDone);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveIncomingRequestEvent>().Subscribe(request =>
            {
                var targetRequest = IncomingRequests.SingleOrDefault(x => x.Id == request.Id);
                targetRequest?.Refresh(request);
            });

            Load();
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            if (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                GlobalAppProperties.User.RoleCurrent == Role.Director)
            {
                //все сохраненные входящие запросы
                var requests = UnitOfWork.Repository<IncomingRequest>().GetAll();
                //все входящие документы
                var incomingDocuments = UnitOfWork.Repository<Document>().Find(x => x.RecipientEmployee.Company.Id == GlobalAppProperties.Actual.OurCompany.Id);

                //непорученные никому входящие документы
                var targetDocuments = incomingDocuments.Except(requests.Select(x => x.Document)).ToList();
                var targetRequests = targetDocuments.Select(x => new IncomingRequest { Document = x });

                //обновляем коллекцию
                IncomingRequests.Clear();
                IncomingRequests.AddRange(requests.Union(targetRequests).OrderBy(x => x.Document.Date).Select(x => new IncomingRequestLookup(x)));
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                //все сохраненные входящие запросы
                var requests = UnitOfWork.Repository<IncomingRequest>().Find(x => x.Performers.ContainsById(GlobalAppProperties.User));

                //обновляем коллекцию
                IncomingRequests.Clear();
                IncomingRequests.AddRange(requests.Select(x => new IncomingRequestLookup(x)));
            }
        }
    }
}