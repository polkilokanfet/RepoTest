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
    public partial class SpecificationsView
    {
        private readonly IUnityContainer _container;
        private readonly SpecificationsViewModel _viewModel;

        public SpecificationsView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
            _viewModel = container.Resolve<SpecificationsViewModel>();
            InitializeComponent();
            this.DataContext = _viewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var units = _container.Resolve<IUnitOfWork>().Repository<SalesUnit>().Find(x => x.Specification != null && x.Project.Manager.Id == CommonOptions.User.Id);
            var specs = units.Select(x => x.Specification).Distinct().ToList();
            var lookups = specs.Select(x => new SpecificationLookup(x, units.Where(u => u.Specification.Id == x.Id)));
            _viewModel.Load(lookups);
            this.Loaded -= OnLoaded;
        }
    }
}
