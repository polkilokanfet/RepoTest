using System;
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
        public ProjectDetailsViewModel ProjectDetailsViewModel { get; }

        public MarketViewModel(ProjectListViewModel projectListViewModel, ProjectDetailsViewModel projectDetailsViewModel)
        {
            ProjectListViewModel = projectListViewModel;
            ProjectDetailsViewModel = projectDetailsViewModel;

            ProjectListViewModel.SelectedLookupChanged += ProjectListViewModelOnSelectedLookupChanged;
        }

        private async void ProjectListViewModelOnSelectedLookupChanged(ProjectLookup projectLookup)
        {
            await ProjectDetailsViewModel.LoadAsync(projectLookup.Id);
        }
    }
}
