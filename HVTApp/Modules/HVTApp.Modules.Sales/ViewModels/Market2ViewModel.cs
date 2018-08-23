using System.Linq;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel
    {
        public ProjectListViewModel ProjectListViewModel { get; }
        public OfferListViewModel OfferListViewModel { get; }
        public SalesUnitListViewModel SalesUnitListViewModel { get; }

        public Market2ViewModel(ProjectListViewModel projectListViewModel, OfferListViewModel offerListViewModel, SalesUnitListViewModel salesUnitListViewModel, 
            IEventAggregator eventAggregator)
        {
            ProjectListViewModel = projectListViewModel;
            OfferListViewModel = offerListViewModel;
            SalesUnitListViewModel = salesUnitListViewModel;

            ProjectListViewModel.SelectedLookupChanged += project =>
            {
                var salesUnitLookups = project.Entity.SalesUnits.Select(x => new SalesUnitLookup(x));
                SalesUnitListViewModel.Load(salesUnitLookups);

                var offerLookups = project.Entity.Offers.Select(x => new OfferLookup(x));
                OfferListViewModel.Load(offerLookups);
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
