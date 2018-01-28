using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel : BindableBase
    {
        private readonly WrapperDataService _wrapperDataService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _container;
        private readonly IUpdateDetailsService _updateDetailsService;
        private bool _loadedFlag = false;
        private ProjectWrapper _selectedProject;
        private OfferWrapper _selectedOffer;

        public MarketViewModel(WrapperDataService wrapperDataService, IEventAggregator eventAggregator, IUnityContainer container)
        {
            _wrapperDataService = wrapperDataService;
            _eventAggregator = eventAggregator;
            _container = container;
            _updateDetailsService = _container.Resolve<IUpdateDetailsService>();

            EditProjectCommand = new DelegateCommand(OnEditProjectCommand_Execute);
            EditTenderCommand = new DelegateCommand(OnEditTenderCommand_Execute);
            EditOfferCommand = new DelegateCommand(OnEditOfferCommand_Execute);

            _eventAggregator.GetEvent<AfterSaveCompanyEvent>().Subscribe(OnSaveCompany);

            LoadedCommand = new DelegateCommand(async () =>
            {
                if(!_loadedFlag) Load();
                _loadedFlag = true;
            });
        }

        private void OnEditProjectCommand_Execute()
        {
            _updateDetailsService.UpdateDetails<Project>(SelectedProject.Model.Id);
        }

        private void OnEditTenderCommand_Execute()
        {
            throw new NotImplementedException();
        }

        private void OnEditOfferCommand_Execute()
        {
            _updateDetailsService.UpdateDetails<Offer>(SelectedOffer.Id);
        }

        public ICommand NewProjectCommand { get; }
        public ICommand NewTenderCommand { get; }
        public ICommand NewOfferCommand { get; }


        public ICommand EditProjectCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand EditOfferCommand { get; }

        public ICommand RemoveProjectCommand { get; }
        public ICommand RemoveTenderCommand { get; }
        public ICommand RemoveOfferCommand { get; }

        private void OnSaveCompany(Company company)
        {
            foreach (var project in Projects)
            {
                project.Refresh();
                if (project.Tenders.Any() && Equals(project.Tenders.First().Winner.Model, company))
                    project.Tenders.First().Refresh();
            }
        }

        public ICommand LoadedCommand { get; }

        public ObservableCollection<ProjectWrapper> Projects { get; } = new ObservableCollection<ProjectWrapper>();

        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }

        private async void Load()
        {
            var projectWrappers = await _wrapperDataService.ProjectWrapperDataService.GetAllAsync();
            Projects.AddRange(projectWrappers);
        }

        public OfferWrapper SelectedOffer
        {
            get { return _selectedOffer; }
            set
            {
                _selectedOffer = value;
                OnPropertyChanged();
            }
        }
    }
}
