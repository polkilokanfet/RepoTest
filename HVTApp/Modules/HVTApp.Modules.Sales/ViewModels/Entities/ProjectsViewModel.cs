using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.DataAccess.Lookup;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectsViewModel : BaseListViewModel<ProjectWrapper, Project, ProjectDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectLookupDataService _projectLookupDataService;

        private bool _loaded = false;

        public ProjectsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService, IProjectLookupDataService projectLookupDataService) : base(container)
        {
            _unitOfWork = unitOfWork;
            _projectLookupDataService = projectLookupDataService;

            //unitOfWork.Projects.GetAll().Select(x => new ProjectWrapper(x)).ForEach(Items.Add);

            LoadedCommand = new DelegateCommand(async () => 
            {
                if (!_loaded) await LoadAsync();
                _loaded = true;
            });
        }

        public DelegateCommand LoadedCommand { get; }

        public ObservableCollection<ProjectLookup> ProjectLookups { get; } = new ObservableCollection<ProjectLookup>();

        public async Task LoadAsync()
        {
            var projectLookups = await _projectLookupDataService.GetProjectLookupsAsync();
            ProjectLookups.Clear();
            ProjectLookups.AddRange(projectLookups);
        }
    }
}
