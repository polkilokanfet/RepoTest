using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.Lookup;
using HVTApp.UI.Tabs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    [RibbonTab(typeof(TabOffer))]
    public partial class OffersView
    {
        public OffersView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            var units = container.Resolve<IUnitOfWork>().Repository<OfferUnit>().Find(x => x.Offer.Project.Manager.Id == CommonOptions.User.Id);
            var offers = units.Select(x => x.Offer).Distinct().ToList();
            var lookups = offers.Select(x => new OfferLookup(x, units.Where(u => u.Offer.Id == x.Id)));
            var offersViewModel = container.Resolve<OffersViewModel>();
            offersViewModel.Load(lookups.OrderBy(x => x.Date));
            this.DataContext = offersViewModel;
        }
    }
}
