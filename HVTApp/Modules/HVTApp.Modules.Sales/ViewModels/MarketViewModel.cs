using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectListViewModel ProjectListViewModel { get; }

        public ObservableCollection<UnitGroup> UnitGroups { get; } = new ObservableCollection<UnitGroup>();
        public ObservableCollection<Tender> Tenders { get; } = new ObservableCollection<Tender>();

        public MarketViewModel(ProjectListViewModel projectListViewModel, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ProjectListViewModel = projectListViewModel;

            ProjectListViewModel.SelectedLookupChanged += ProjectListViewModelOnSelectedLookupChanged;
        }



        private void ProjectListViewModelOnSelectedLookupChanged(ProjectLookup projectLookup)
        {
            UnitGroups.Clear();
            UnitGroups.AddRange(projectLookup.Entity.SalesUnits.Select(x => new SalesUnitWrapper(x)).ToUnitGroups());

            Tenders.Clear();
            Tenders.AddRange(_unitOfWork.GetRepository<Tender>().Find(x => x.Project.Id == projectLookup.Entity.Id));
        }
    }
}
