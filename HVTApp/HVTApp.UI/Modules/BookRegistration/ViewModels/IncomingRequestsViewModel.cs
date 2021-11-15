using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.BookRegistration.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestsViewModel : ViewModelBaseCanExportToExcel
    {
        private readonly IFileManagerService _fileManagerService;
        private IncomingRequestLookup _selectedIncomingRequest;

        public bool IsDirectorView => GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director;
        public bool IsManagerView => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;

        public ObservableCollection<IncomingRequestLookup> IncomingRequests { get; } = new ObservableCollection<IncomingRequestLookup>();

        public IncomingRequestLookup SelectedIncomingRequest
        {
            get => _selectedIncomingRequest;
            set
            {
                if (Equals(_selectedIncomingRequest, value))
                    return;
                
                _selectedIncomingRequest = value;
                InstructRequestCommand.RaiseCanExecuteChanged();
                OpenFolderCommand.RaiseCanExecuteChanged();
                RequestIsDoneCommand.RaiseCanExecuteChanged();

                SelectedIncomingRequestChanged?.Invoke(value?.Entity);
            }
        }

        public event Action<IncomingRequest> SelectedIncomingRequestChanged;

        //перезагрузить
        public DelegateLogCommand ReloadCommand { get; }       
        //поручить запрос
        public DelegateLogCommand InstructRequestCommand { get; }

        public DelegateLogCommand OpenFolderCommand { get; }

        public DelegateLogCommand RequestIsDoneCommand { get; }

        public IncomingRequestsViewModel(IUnityContainer container) : base(container)
        {
            _fileManagerService = container.Resolve<IFileManagerService>();

            ReloadCommand = new DelegateLogCommand(Load);

            InstructRequestCommand = new DelegateLogCommand(
                () =>
                {
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<IncomingRequestView>(
                            new NavigationParameters
                            {
                                {"Model", SelectedIncomingRequest.Entity},
                                {"UnitOfWork", UnitOfWork}
                            });
                },
                () => SelectedIncomingRequest != null && IsDirectorView);

            OpenFolderCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrEmpty(GlobalAppProperties.Actual.IncomingRequestsPath))
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Информация", "Путь к хранилищу приложений не назначен");
                        return;
                    }

                    var path = _fileManagerService.GetPath(SelectedIncomingRequest.Entity.Document);
                    Process.Start("explorer", $"\"{path}\"");
                },
                () => SelectedIncomingRequest != null);

            RequestIsDoneCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var request = unitOfWork.Repository<IncomingRequest>().GetById(SelectedIncomingRequest.Id);
                    request.DoneDate = DateTime.Now;

                    if (unitOfWork.SaveChanges().OperationCompletedSuccessfully)
                    {
                        SelectedIncomingRequest.Refresh(request);
                        Container.Resolve<IEventAggregator>().GetEvent<AfterSaveIncomingRequestEvent>().Publish(request);
                    }

                    RequestIsDoneCommand.RaiseCanExecuteChanged();
                },
                () => SelectedIncomingRequest != null && !SelectedIncomingRequest.Entity.IsDone);

            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AfterSaveIncomingRequestEvent>().Subscribe(request =>
            {
                var targetRequest = IncomingRequests.SingleOrDefault(x => x.Document.Id == request.Document.Id);
                if (targetRequest != null)
                {
                    targetRequest.Refresh(request);
                    return;
                }

                if (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                    GlobalAppProperties.User.RoleCurrent == Role.Director)
                {
                    IncomingRequests.Add(new IncomingRequestLookup(request));
                    return;
                }

                if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager &&
                    request.Performers.Any(x => x.Id == GlobalAppProperties.User.Employee.Id))
                {
                    IncomingRequests.Add(new IncomingRequestLookup(request));
                    return;
                }
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