using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel : BindableBase
    {
        public ProjectListViewModel ProjectListViewModel { get; }
        public ProjectUnitListViewModel ProjectUnitListViewModel { get; }

        private readonly IUnitOfWork _unitOfWork;

        public MarketViewModel(IUnitOfWork unitOfWork, ProjectListViewModel projectListViewModel, ProjectUnitListViewModel projectUnitListViewModel)
        {
            ProjectListViewModel = projectListViewModel;
            ProjectUnitListViewModel = projectUnitListViewModel;
            _unitOfWork = unitOfWork;

            ProjectListViewModel.PropertyChanged += ProjectListViewModelOnPropertyChanged;
        }

        private async void ProjectListViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ProjectListViewModel.SelectedLookup)) return;

            var projectUnits = _unitOfWork.GetRepository<ProjectUnit>().Find(x => x.ProjectId == ProjectListViewModel.SelectedLookup?.Id);
            await ProjectUnitListViewModel.LoadAsync(projectUnits.Select(x => new ProjectUnitLookup(x)));

        }
    }
}
