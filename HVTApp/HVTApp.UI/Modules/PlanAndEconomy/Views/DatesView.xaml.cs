using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.Tabs;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabDates))]
    public partial class DatesView
    {
        private readonly DatesViewModel _viewModel;

        public DatesView(DatesViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = _viewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.Load();
            this.Loaded -= OnLoaded;
        }
    }
}
