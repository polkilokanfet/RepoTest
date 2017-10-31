using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel
    {
        private readonly WrapperDataService _wrapperDataService;
        private readonly IEventAggregator _eventAggregator;
        private bool _loadedFlag = false;
        private ProjectWrapper _selectedProject;

        public MarketViewModel(WrapperDataService wrapperDataService, IEventAggregator eventAggregator)
        {
            _wrapperDataService = wrapperDataService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AfterSaveCompanyEvent>().Subscribe(OnSaveCompany);

            LoadedCommand = new DelegateCommand(async () =>
            {
                if(!_loadedFlag) await LoadAsync();
                _loadedFlag = true;
            });
        }

        private void OnSaveCompany(Company company)
        {
            foreach (var project in Projects)
            {
                project.Refresh();
                if(project.Tenders.Any() && Equals(project.Tenders.First().Winner.Model, company))
                    project.Tenders.First().Refresh();
            }
        }

        public ICommand LoadedCommand { get; }

        public ObservableCollection<ProjectWrapper> Projects { get; } = new ObservableCollection<ProjectWrapper>();

        public ProjectWrapper SelectedProject
        {
            get { return _selectedProject; }
            set { _selectedProject = value; }
        }

        private async Task LoadAsync()
        {
            var lookups = await _wrapperDataService.ProjectWrapperDataService.GetAllAsync();
            Projects.AddRange(lookups);
        }
    }
}
