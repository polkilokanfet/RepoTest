using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel
    {
        private readonly ProjectWrapperDataService _projectWrapperDataService;
        private bool _loadedFlag = false;

        public MarketViewModel(ProjectWrapperDataService projectWrapperDataService)
        {
            _projectWrapperDataService = projectWrapperDataService;
            Projects = new ObservableCollection<ProjectWrapper>();

            LoadedCommand = new DelegateCommand(async () =>
            {
                if(!_loadedFlag) await LoadAsync();
                _loadedFlag = true;
            });
        }

        public ICommand LoadedCommand { get; }

        public ObservableCollection<ProjectWrapper> Projects { get; }

        public ProjectWrapper SelectedProject { get; set; }

        private async Task LoadAsync()
        {
            var lookups = await _projectWrapperDataService.GetAllWrappersAsync();
            Projects.AddRange(lookups);
        }
    }
}
