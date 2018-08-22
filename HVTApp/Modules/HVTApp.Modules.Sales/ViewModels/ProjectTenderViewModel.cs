using System.Linq;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectTenderViewModel
    {
        public ProjectListViewModel ProjectListViewModel { get; }
        public SalesUnitListViewModel SalesUnitListViewModel { get; }

        public ProjectTenderViewModel(ProjectListViewModel projectListViewModel, SalesUnitListViewModel salesUnitListViewModel)
        {
            ProjectListViewModel = projectListViewModel;
            SalesUnitListViewModel = salesUnitListViewModel;

            ProjectListViewModel.SelectedLookupChanged += project =>
            {
                var lookups = project.Entity.SalesUnits.Select(x => new SalesUnitLookup(x));
                SalesUnitListViewModel.Load(lookups);
            };
        }
    }
}
