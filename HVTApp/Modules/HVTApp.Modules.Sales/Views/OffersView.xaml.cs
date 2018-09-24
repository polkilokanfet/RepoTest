using System.Linq;
using System.Windows;
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
        private readonly IUnityContainer _container;
        private readonly OffersViewModel _offersViewModel;

        public OffersView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
            InitializeComponent();
            _offersViewModel = container.Resolve<OffersViewModel>();
            this.DataContext = _offersViewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var units = await _container.Resolve<IUnitOfWork>().Repository<OfferUnit>().GetAllAsync();
            units = units.Where(x => x.Offer.Project.Manager.Id == CommonOptions.User.Id).ToList();
            var offers = units.Select(x => x.Offer).Distinct().ToList();
            var lookups = offers.Select(x => new OfferLookup(x, units.Where(u => u.Offer.Id == x.Id)));
            _offersViewModel.Load(lookups.OrderBy(x => x.Date));
            Loaded -= OnLoaded;
        }
    }
}
