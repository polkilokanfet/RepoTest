using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.UI.Lookup;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel
    {
        private readonly IProjectLookupDataService _projectLookupDataService;

        private bool _loadedFlag = false;

        public MarketViewModel(IProjectLookupDataService projectLookupDataService)
        {
            _projectLookupDataService = projectLookupDataService;

            Projects = new ObservableCollection<ProjectLookup>();

            LoadedCommand = new DelegateCommand(async () =>
            {
                if(!_loadedFlag) await LoadAsync();
                _loadedFlag = true;
            });
        }

        public ICommand LoadedCommand { get; }

        public ObservableCollection<ProjectLookup> Projects { get; }

        public ProjectLookup SelectedProject { get; set; }

        private async Task LoadAsync()
        {
            var lookups = await _projectLookupDataService.GetAllLookupsAsync();
            Projects.AddRange(lookups);
        }
    }
}
