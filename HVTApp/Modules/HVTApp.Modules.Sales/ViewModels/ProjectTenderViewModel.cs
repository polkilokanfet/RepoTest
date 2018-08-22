using System.Linq;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectTenderViewModel
    {
        public ProjectListViewModel ProjectListViewModel { get; }
        public SalesUnitListViewModel SalesUnitListViewModel { get; }

        public ProjectTenderViewModel(ProjectListViewModel projectListViewModel, SalesUnitListViewModel salesUnitListViewModel, IEventAggregator eventAggregator)
        {
            ProjectListViewModel = projectListViewModel;
            SalesUnitListViewModel = salesUnitListViewModel;

            ProjectListViewModel.SelectedLookupChanged += project =>
            {
                var lookups = project.Entity.SalesUnits.Select(x => new SalesUnitLookup(x));
                SalesUnitListViewModel.Load(lookups);
            };

            eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
            {
                var proj = ProjectListViewModel.Lookups.SingleOrDefault(x => x.Entity.SalesUnits.Contains(salesUnit));

                //костыль для синхронизации сущностей
                proj?.Entity.SalesUnits.Remove(salesUnit);
                proj?.Entity.SalesUnits.Add(salesUnit);

                proj?.Refresh();
            });
        }

    }
}
