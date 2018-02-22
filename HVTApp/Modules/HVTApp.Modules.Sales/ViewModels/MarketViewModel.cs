using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
        public ProjectListViewModel ProjectListViewModel { get; }

        public ObservableCollection<UnitGroupGroup> ProjectUnitGroups { get; } = new ObservableCollection<UnitGroupGroup>();

        public MarketViewModel(ProjectListViewModel projectListViewModel, ProjectDetailsViewModel projectDetailsViewModel)
        {
            ProjectListViewModel = projectListViewModel;

            ProjectListViewModel.SelectedLookupChanged += ProjectListViewModelOnSelectedLookupChanged;
        }



        private void ProjectListViewModelOnSelectedLookupChanged(ProjectLookup projectLookup)
        {
            ProjectUnitGroups.Clear();
            ProjectUnitGroups.AddRange(projectLookup.Entity.SalesUnits.Select(x => new SalesUnitGroupWrapper(x)).ToUnitGroups());
        }
    }
}
