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
        public SalesUnitListViewModel SalesUnitListViewModel { get; }

        private readonly IUnitOfWork _unitOfWork;

        public MarketViewModel(IUnitOfWork unitOfWork, ProjectListViewModel projectListViewModel, SalesUnitListViewModel salesUnitListViewModel)
        {
            ProjectListViewModel = projectListViewModel;
            SalesUnitListViewModel = salesUnitListViewModel;
            _unitOfWork = unitOfWork;

            ProjectListViewModel.PropertyChanged += ProjectListViewModelOnPropertyChanged;
        }

        private async void ProjectListViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ProjectListViewModel.SelectedLookup)) return;

            var salesUnits = ProjectListViewModel.SelectedLookup.Entity.SalesUnits;
            await SalesUnitListViewModel.LoadAsync(salesUnits.Select(x => new SalesUnitLookup(x)));

        }
    }
}
