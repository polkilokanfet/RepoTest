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
        public UnitLookupListViewModel UnitLookupListViewModel { get; }

        public Market2ViewModel(ProjectListViewModel projectListViewModel, OfferListViewModel offerListViewModel, UnitLookupListViewModel unitLookupListViewModel, 
            IEventAggregator eventAggregator)
        {
            ProjectListViewModel = projectListViewModel;
            OfferListViewModel = offerListViewModel;
            UnitLookupListViewModel = unitLookupListViewModel;

            ProjectListViewModel.SelectedLookupChanged += project =>
            {
                var salesUnitLookups = project.Entity.SalesUnits.Select(x => new SalesUnitLookup(x));
                UnitLookupListViewModel.Load(salesUnitLookups);

                var offerLookups = project.Entity.Offers.Select(x => new OfferLookup(x));
                OfferListViewModel.Load(offerLookups);
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                var lookups = offer?.Entity.OfferUnits.Select(x => new OfferUnitLookup(x));
                UnitLookupListViewModel.Load(lookups);
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
